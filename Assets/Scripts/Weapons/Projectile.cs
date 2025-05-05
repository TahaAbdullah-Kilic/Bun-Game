using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 20f;
    [SerializeField] GameObject ParticleOnHitVFX;
    [SerializeField] bool IsEnemyProjectile = false;
    [SerializeField] float projectileRange = 10f;
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        UpdateProjectileRange();
        DetectDistance();
    }
    public void UpdateWeaponInfo(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }
    void UpdateProjectileRange()
    {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if(!collision.isTrigger &&(enemyHealth || indestructible || player))
        {
            if(player && IsEnemyProjectile)
            {
                player.TakeDamage(1, transform);
            }
            Destroy(gameObject);
            Instantiate(ParticleOnHitVFX, transform.position, transform.rotation);
        }
    }
    void DetectDistance()
    {
        if(Vector3.Distance(transform.position, startPosition) > projectileRange) Destroy(gameObject);
    }
}
