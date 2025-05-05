using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 20f;
    [SerializeField] GameObject ParticleOnHitVFX;
    WeaponInfo weaponInfo;
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        MoveProjectile();
        DetectDistance();
    }
    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }
    void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible= collision.gameObject.GetComponent<Indestructible>();
        if(!collision.isTrigger &&(enemyHealth || indestructible))
        {
            Destroy(gameObject);
            Instantiate(ParticleOnHitVFX, transform.position, transform.rotation);
        }
    }
    void DetectDistance()
    {
        if(Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange) Destroy(gameObject);
    }
}
