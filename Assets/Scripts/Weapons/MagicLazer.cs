using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MagicLazer : MonoBehaviour
{
    [SerializeField] float LaserGrowTime = 0.5f;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    float laserRange;
    bool isGrowing = true;
    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        LaserFaceMouse();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            isGrowing = false;
        }
    }
    IEnumerator IncreaseLaserRangeRoutine()
    {
        float timePassed = 0f;
        while(spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float GrowTime = timePassed / LaserGrowTime;
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange,GrowTime),1f);
            capsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, GrowTime), capsuleCollider.size.y);
            capsuleCollider.offset = new Vector2(capsuleCollider.size.x/2,capsuleCollider.offset.y);
            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().FadeRoutine());
    }
    void LaserFaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        transform.right = direction;
    }
    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserRangeRoutine());
    }
}
