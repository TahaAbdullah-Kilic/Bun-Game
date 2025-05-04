using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    [SerializeField] float flashTime;
    Material defaultMaterial;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material = defaultMaterial;
    }
    public float GetFlashTime()
    {
        return flashTime;
    }
}
