using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] float MoveSpeed;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] GameObject WeaponCollider;
    [SerializeField] Transform AttackAnimationSpawnPoint;
    [SerializeField] float DashSpeed = 2.5f;
    public bool FacingRight { get { return facingRight; } }
    Animator animator;
    float moveX, moveY;
    SpriteRenderer spriteRenderer;
    bool facingRight = true;
    
    bool isDashing = false;
    private float defaultMoveSpeed;
    Knockback knockback;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }
    void Start()
    {
        defaultMoveSpeed = MoveSpeed;
        ActiveInventorySlot.Instance.EquipStartingWeapon();
    }
    void Update()
    {
        Animation();
        moveX = Input.GetAxis("Horizontal") * MoveSpeed;
        moveY = Input.GetAxis("Vertical") * MoveSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
        if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
    }
    void FixedUpdate()
    {
        Move();
        PlayerDirection();
    }

    private void Move()
    {
        if(knockback.GettingKnockbacked || PlayerHealth.Instance.IsDead) return;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveX, moveY);
    }

    public GameObject GetWeaponCollider()
    {
        return WeaponCollider;
    }
    public Transform GetAttackAnimationSpawnPoint()
    {
        return AttackAnimationSpawnPoint;
    }
    void Animation()
    {
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }
    void PlayerDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePosition.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        } 
        else
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        } 
    }
    void Dash()
    {
        if(!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();
            isDashing = true;
            trailRenderer.emitting = true;
            MoveSpeed *= DashSpeed;
            StartCoroutine(DashRoutine());
        }
    }
    IEnumerator DashRoutine()
    {
        float dashDuration = 0.2f;
        float dashCooldown = 0.25f;
        yield return new WaitForSeconds(dashDuration);
        MoveSpeed = defaultMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
