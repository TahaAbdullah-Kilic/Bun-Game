using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool IsDead { get; private set; }
    [SerializeField] float MaxHealth = 30;
    [SerializeField] float KnockbackAmount = 10f;
    float currentHealth;
    Knockback knockback;
    Flash flash;
    IFrame iframe;
    Slider healthBar;
    const string HEALTH_BAR_TEXT = "Health Bar";
    const string VILLAGE_TEXT = "Starter_Village";
    Animator animator;
    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        iframe = GetComponent<IFrame>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        IsDead = false;
        currentHealth = MaxHealth;
        UpdateHealthBar();
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
        UpdateHealthBar();
        IsPlayerDead();
    }
    void IsPlayerDead()
    {
        if(currentHealth <= 0 && !IsDead)
        {
            IsDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            DeathAnimation();
            StartCoroutine(DeathLoadSceneRoutine());

        }
    }
    IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Stamina.Instance.ReplenishStaminaOnDeath();
        SceneManager.LoadScene(VILLAGE_TEXT);
    }
    public void HealPlayer()
    {
        if(currentHealth < MaxHealth) currentHealth += 1;
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        if(healthBar == null) healthBar = GameObject.Find(HEALTH_BAR_TEXT).GetComponent<Slider>();
        
        healthBar.maxValue = MaxHealth;
        healthBar.value = currentHealth;
    }
    void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
}
