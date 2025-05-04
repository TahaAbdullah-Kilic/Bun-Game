using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum EnemyState
    {
        Roaming
    }
    [SerializeField] float roamMoveTime;
    [SerializeField] float roamWaitTime;
    EnemyState state;
    EnemyPathfinding pathfinding;
    Knockback knockback;
    void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = EnemyState.Roaming;
        knockback = GetComponent<Knockback>();
    }
    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }
    IEnumerator RoamingRoutine()
    {
        while (state == EnemyState.Roaming)
        {
            Vector2 roamingDirection = GetRoamingDirection();
            pathfinding.MoveDirection(roamingDirection);
            yield return new WaitForSeconds(roamMoveTime);
            pathfinding.MoveDirection(new Vector2(0,0));
            yield return new WaitForSeconds(roamWaitTime);

        }
    }
    Vector2 GetRoamingDirection()
    {
        return new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
    }
}
