using System.Collections;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] float MaxHealth = 30;
    [SerializeField] float KnockbackAmount = 10f;
    float currentHealth;
    Knockback knockback;
    Flash flash;
    IFrame iframe;
    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        iframe = GetComponent<IFrame>();
    }
    void Start()
    {
        currentHealth = MaxHealth;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if(enemy)
        {
            TakeDamage(1, collision.transform);
        }
    }
    public void TakeDamage(float damageAmount, Transform hitTransform)
    {
        //ScreenShakeManager.Instance.ShakeScreen();
        if(!iframe.GetcanTakeDamage()) return;
        iframe.SetcanTakeDamage(false);
        currentHealth -= damageAmount;
        knockback.GetKnockbacked(hitTransform.gameObject.transform, KnockbackAmount);
        StartCoroutine(iframe.TakeDamageRoutine());
        StartCoroutine(flash.FlashRoutine());
    }
    public void HealPlayer()
    {
        currentHealth += 1;
    }
}
