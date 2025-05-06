using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] float Duration = 1f;
    [SerializeField] AnimationCurve animatonCurve;
    [SerializeField] float HeightY = 3f;
    [SerializeField] GameObject Shadow;
    [SerializeField] GameObject SplatterPrefab;
    void Start()
    {
        GameObject shadow = Instantiate(Shadow, transform.position +new Vector3(0, -0.6f, 0), Quaternion.identity);
        
        Vector3 playerPosition = PlayerController.Instance.transform.position;
        Vector3 shadowSpawnPosition = shadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPosition));
        StartCoroutine(MoveGrapeShadowRoutine(shadow, shadowSpawnPosition, playerPosition + new Vector3(0,-0.4f,0)));
    }
    IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < Duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / Duration;
            float heighT = animatonCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, HeightY, heighT);

            transform.position = Vector2.Lerp(startPosition, endPosition,linearT) + new Vector2(0f, height);   

            yield return null;
        }
    Instantiate(SplatterPrefab, transform.position + new Vector3(0, -0.4f, 0), Quaternion.identity);
    Destroy(gameObject);
    }
    IEnumerator MoveGrapeShadowRoutine(GameObject shadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while(timePassed < Duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / Duration;
            
            shadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
            yield return null;
        }
        Destroy(shadow);
    }
}
