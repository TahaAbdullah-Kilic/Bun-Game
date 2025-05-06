using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum EnemyState
    {
        Roaming,
        Attacking
    }
    [SerializeField] float roamMoveTime = 2f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] MonoBehaviour EnemyType;
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] bool StopMovingWhenAttacking = false;
    EnemyState state;
    EnemyPathfinding pathfinding;
    Vector2 roamingDirection;
    float roamMoveCounter;
    bool cooldown = false;
    void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = EnemyState.Roaming;
    }
    void Start()
    {
        roamingDirection = GetRoamingDirection();
    }
    void Update()
    {
        MovementStateController();
    }
    void MovementStateController()
    {
        switch (state)
        {
            default:
            case EnemyState.Roaming:
            Roaming();
            break;

            case EnemyState.Attacking:
            Attacking();
            break;
        }
    }
    void Roaming()
    {
            roamMoveCounter += Time.deltaTime;
            
            pathfinding.MoveDirection(roamingDirection);

            if(Vector2.Distance(PlayerController.Instance.transform.position, transform.position) < attackRange)
            {
                state = EnemyState.Attacking;
            }


            if(roamMoveCounter > roamMoveTime)
            {
                roamingDirection = GetRoamingDirection();
                roamMoveCounter = 0f;
            } 
    }
    void Attacking()
    {
        if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > attackRange)
        {
            state = EnemyState.Roaming;
        }
        if (!cooldown && EnemyType && attackRange != 0) 
        {
            cooldown = true;
            (EnemyType as IEnemy).Attack();

            if(StopMovingWhenAttacking) pathfinding.StopMoving();
            else pathfinding.MoveDirection(GetRoamingDirection());
            
            StartCoroutine(AttackCDRoutine());
        }
    }
    IEnumerator AttackCDRoutine()
    {
            yield return new WaitForSeconds(timeBetweenAttacks);
            cooldown = false;
    }
    Vector2 GetRoamingDirection()
    {
        return new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
    }
}
