using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject GrapeProjectilePrefab;
    Animator animator;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        if(transform.position.x - PlayerController.Instance.transform.position.x < 0) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }
    public void SpawnProjectileAnimation()
    {
        Instantiate(GrapeProjectilePrefab,transform.position, Quaternion.identity);
    }
}
