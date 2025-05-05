using System.Collections;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfo WeaponInfo;
    [SerializeField] GameObject MagicLazer;
    [SerializeField] Transform MagicLazerSpawnPoint;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MouseFollowWithOnset();
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        ActiveWeapon.Instance.AttackCooldown();
    }
    public WeaponInfo GetWeaponInfo()
    {
        return WeaponInfo;
    }
    public void SpawnStaffProjectileAnimation()
    {
        GameObject laser = Instantiate(MagicLazer, MagicLazerSpawnPoint.position, Quaternion.identity);
        laser.GetComponent<MagicLazer>().UpdateLaserRange(WeaponInfo.weaponRange);
    }
    void MouseFollowWithOnset()
    {
        Vector3 mousePosition = Input.mousePosition;
        //Vector3 playerPosition = PlayerController.Instance.transform.position;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        //float angle = Mathf.Atan2(mousePosition.y - playerPosition.y, MathF.Abs(mousePosition.x - playerPosition.x)) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(mousePosition.y,mousePosition.x) * Mathf.Rad2Deg;
        if (mousePosition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}
