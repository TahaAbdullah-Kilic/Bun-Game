using System;
using System.Collections;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] float damage;
    EnemyHealth enemyHealth;
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.GetComponent<EnemyHealth>())
        {
            enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
