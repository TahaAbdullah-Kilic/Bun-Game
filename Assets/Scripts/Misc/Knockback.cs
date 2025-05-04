using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockbacked {get; private set;}
    [SerializeField] float KnockbackTime;
    Rigidbody2D rigidBody;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    public void GetKnockbacked(Transform damageSource, float KnockbackAmount)
    {
        GettingKnockbacked = true;
        Vector2 knockbackDirection = (transform.position - damageSource.position).normalized * KnockbackAmount * rigidBody.mass;
        rigidBody.AddForce(knockbackDirection, ForceMode2D.Impulse);
        StartCoroutine(KnockbackRoutine());
    }

    IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(KnockbackTime);
        rigidBody.linearVelocity = Vector2.zero;   
        GettingKnockbacked = false;
    }
}
