using System;
using System.Collections;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    float damage;
    void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damage = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damage);
        }
    }
}
