using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    Rigidbody2D rigidBody;
    Vector2 moveDirection;
    Knockback knockback;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        knockback = GetComponent<Knockback>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (knockback.GettingKnockbacked) return;
        rigidBody.MovePosition(rigidBody.position + moveDirection * (MoveSpeed * Time.deltaTime));
    }
    public void MoveDirection(Vector2 newDirection)
    {
        moveDirection = newDirection;
        if(moveDirection.x < 0) spriteRenderer.flipX = true;
        else if(moveDirection.x > 0) spriteRenderer.flipX = false;
    }
    public void StopMoving()
    {
        moveDirection = Vector3.zero;
    }
}
