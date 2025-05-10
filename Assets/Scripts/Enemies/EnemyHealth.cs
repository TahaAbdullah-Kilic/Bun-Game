using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    [SerializeField] GameObject DeathVFXPrefab;
    [SerializeField] float KnockbackAmount;
    float currentHealth;
    Knockback knockback;
    Flash flash;
    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(flash.GetFlashTime());
        DetechDeath();
    }
    void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    void Start()
    {
        currentHealth = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        knockback.GetKnockbacked(PlayerController.Instance.transform, KnockbackAmount);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(DieRoutine());       
    }
    void DetechDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(DeathVFXPrefab,transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
            Destroy(gameObject);
        } 
    }
}
