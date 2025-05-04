using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] float MoveSpeed;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] GameObject WeaponCollider;
    [SerializeField] Transform AttackAnimationSpawnPoint;
    public bool FacingRight { get { return facingRight; } }
    Animator animator;
    float moveX, moveY;
    SpriteRenderer spriteRenderer;
    bool facingRight = true;
    float dashSpeed = 5f;
    bool isDashing = false;
    private float defaultMoveSpeed;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMoveSpeed = MoveSpeed;
    }
    void Update()
    {
        Animation();
        moveX = Input.GetAxis("Horizontal") * MoveSpeed;
        moveY = Input.GetAxis("Vertical") * MoveSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
    }
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveX, moveY);
        PlayerDirection();
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
        if(!isDashing)
        {
            isDashing = true;
            trailRenderer.emitting = true;
            MoveSpeed *= dashSpeed;
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
}
