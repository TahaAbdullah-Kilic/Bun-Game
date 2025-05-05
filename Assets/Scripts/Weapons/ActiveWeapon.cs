using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon {get; private set;}
    bool cooldown = false;
    float timeBetweenAttacks;
    void Update()
    {
        if (Input.GetMouseButton(0)) Attack();
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
        AttackCooldown();   
    }
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }
    void Attack()
    {
        if(cooldown == false && CurrentActiveWeapon) (CurrentActiveWeapon as IWeapon).Attack();
        
    }
    IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        cooldown = false;
    }
    public void AttackCooldown()
    {
        cooldown = true;
        StopAllCoroutines();
        StartCoroutine(AttackCDRoutine());
    }
}
