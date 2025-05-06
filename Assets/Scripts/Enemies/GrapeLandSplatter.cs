using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    SpriteFade spriteFade;
    void Awake()
    {
        spriteFade = GetComponent<SpriteFade>();
    }
    void Start()
    {
        StartCoroutine(spriteFade.FadeRoutine());

        Invoke("DisableCollider", 0.25f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth= collision.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1f, transform);
    }
    void DisableCollider()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
