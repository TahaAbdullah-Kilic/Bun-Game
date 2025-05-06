using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfo WeaponInfo;
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform ArrowSpawnPoint;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        animator.SetTrigger("Fire");
        GameObject arrow = Instantiate(Arrow, ArrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        ActiveWeapon.Instance.AttackCooldown();
        arrow.GetComponent<Projectile>().UpdateProjectileRange(WeaponInfo.weaponRange);
    }
    public WeaponInfo GetWeaponInfo()
    {
        return WeaponInfo;
    }
}
