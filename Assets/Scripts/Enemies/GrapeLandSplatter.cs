using System.Collections;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    [SerializeField] float SplatterTime = 3f;
    SpriteFade spriteFade;
    void Awake()
    {
        spriteFade = GetComponent<SpriteFade>();
    }
    void Start()
    {
        StartCoroutine(SplatterDisableRoutine());
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
    IEnumerator SplatterDisableRoutine()
    {
        yield return new WaitForSeconds(SplatterTime);
        
        StartCoroutine(spriteFade.FadeRoutine());

        Invoke("DisableCollider", 0.25f);
    }
}
