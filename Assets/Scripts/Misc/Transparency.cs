using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transparency : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float TransparencyAmount = 0.5f;
    [SerializeField] float TransparencyFadeTime = 0.5f;
    SpriteRenderer spriteRenderer;
    Tilemap tileMap;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileMap = GetComponent<Tilemap>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            if(gameObject.activeInHierarchy)
            {
                if (spriteRenderer) StartCoroutine(FadeRoutine(spriteRenderer, TransparencyFadeTime, spriteRenderer.color.a, TransparencyAmount));
                else if (tileMap) StartCoroutine(FadeRoutine(tileMap, TransparencyFadeTime, tileMap.color.a, TransparencyAmount));
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if(gameObject.activeInHierarchy)
            {
                if (spriteRenderer) StartCoroutine(FadeRoutine(spriteRenderer, TransparencyFadeTime, spriteRenderer.color.a, 1f));
                else if (tileMap) StartCoroutine(FadeRoutine(tileMap, TransparencyFadeTime, tileMap.color.a, 1f));
            }
        }
    }
    IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while(elapsedTime <= fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha); 
            yield return null;
        }
    }
    IEnumerator FadeRoutine(Tilemap tileMap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime <= fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tileMap.color = new Color(tileMap.color.r, tileMap.color.g, tileMap.color.b, newAlpha);
            yield return null;
        }
    }
}
