using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    Rigidbody2D rigidBody;
    Vector2 moveDirection;
    Knockback knockback;
    void Awake()
    {
        knockback = GetComponent<Knockback>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (knockback.GettingKnockbacked) return;
        rigidBody.MovePosition(rigidBody.position + moveDirection * (MoveSpeed * Time.deltaTime));
    }
    public void MoveDirection(Vector2 newDirection)
    {
        moveDirection = newDirection;
    }
}
