using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float BulletSpeed;
    [SerializeField] int BurstCount;
    [SerializeField] int ProjectilesPerBurst;
    [SerializeField] [Range(0,359)] float AngleSpread;
    [SerializeField] float TimeBetweenBurstAttacks;
    [SerializeField] float TimeBetweenAttacks = 1f;
    [SerializeField] float DistanceFromTransform = 0.1f;
    [SerializeField] bool stagger;
    [SerializeField] bool oscillate;
    bool cooldown = false;

    void OnValidate()
    {
        if(oscillate) stagger = true;
        if(!oscillate) stagger = false;
        if(ProjectilesPerBurst < 1) ProjectilesPerBurst = 1;
        if(BurstCount < 1) BurstCount = 1;
        if(TimeBetweenBurstAttacks <0.1f) TimeBetweenBurstAttacks = 0.1f;
        if(TimeBetweenAttacks < 0.1f) TimeBetweenAttacks = 0.1f;
        if(DistanceFromTransform < 0.1f) DistanceFromTransform = 0.1f;
        if(AngleSpread == 0) ProjectilesPerBurst = 1;
        if(BulletSpeed <= 0) BulletSpeed = 0.1f;
    }

    public void Attack()
    {
        if(!cooldown) StartCoroutine(ShootRoutine());
    }
    IEnumerator ShootRoutine()
    {
        cooldown = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if(stagger) timeBetweenProjectiles = TimeBetweenBurstAttacks / ProjectilesPerBurst;
        for (int i = 0; i < BurstCount; i++)
        {
            if(!oscillate) TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            
            if(oscillate && i % 2 != 1) TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

            else if(oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < ProjectilesPerBurst; j++)
            {
                Vector2 bulletPosition = FindBulletSpawnPosition(currentAngle);

                GameObject bullet = Instantiate(BulletPrefab, bulletPosition, Quaternion.identity);
                bullet.transform.right = bullet.transform.position - transform.position;

                bullet.GetComponent<Projectile>().UpdateProjectileSpeed(BulletSpeed);

                currentAngle += angleStep;

                if(stagger) yield return new WaitForSeconds(timeBetweenProjectiles);
            }
            currentAngle = startAngle;
            if(!stagger) yield return new WaitForSeconds(TimeBetweenBurstAttacks);
        }

        yield return new WaitForSeconds(TimeBetweenAttacks);
        cooldown = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread;
        angleStep = 0f;
        if (AngleSpread != 0)
        {
            angleStep = AngleSpread / (ProjectilesPerBurst - 1);
            halfAngleSpread = AngleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    Vector2 FindBulletSpawnPosition(float currentAngle)
    {
        float x = transform.position.x + DistanceFromTransform * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + DistanceFromTransform * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 position = new Vector2(x,y);
        return position;
    }
}
