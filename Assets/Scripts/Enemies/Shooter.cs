using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float BulletSpeed;
    [SerializeField] int BurstCount;
    [SerializeField] float TimeBetweenBurstAttacks;
    [SerializeField] float timeBetweenAttacks = 1f;
    bool cooldown = false;
    public void Attack()
    {
        if(!cooldown) StartCoroutine(ShootRoutine());
    }
    IEnumerator ShootRoutine()
    {
        cooldown = true;
        for (int i = 0; i < BurstCount; i++)
        {
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.right = targetDirection;

            bullet.GetComponent<Projectile>().UpdateProjectileSpeed(BulletSpeed);

            yield return new WaitForSeconds(TimeBetweenBurstAttacks);
        }
        
        yield return new WaitForSeconds(timeBetweenAttacks);
        cooldown = false;
    }
}
