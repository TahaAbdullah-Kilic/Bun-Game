using Unity.Mathematics;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject DestroyVFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            GetComponent<PickupSpawner>().DropItems();
            Instantiate(DestroyVFX,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
