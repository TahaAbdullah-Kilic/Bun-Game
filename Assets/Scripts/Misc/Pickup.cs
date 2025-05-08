using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    enum PickupType
    {
        GoldCoin,
        HealthPickup,
        StaminaPickup
    }
    [SerializeField] PickupType pickupType;
    [SerializeField] float PickupDistance = 5f;
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float accelerationRate = 0.5f;
    [SerializeField] AnimationCurve AnimationCurve;
    [SerializeField] float HeightY =1.5f;
    [SerializeField] float PopDuration = 1f;
    Vector3 moveDirection;
    Rigidbody2D rigidBody;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(AnimationCurveRoutine());
    }
    void Update()
    {
        Vector3 playerPosition = PlayerController.Instance.transform.position;
        
        if(Vector3.Distance(transform.position, playerPosition) < PickupDistance)
        {
            moveDirection = (playerPosition - transform.position).normalized;
            MoveSpeed += accelerationRate;
        }
        else
        {
            moveDirection = Vector3.zero;
            MoveSpeed = 0f;
        }
    }
    void FixedUpdate()
    {
        rigidBody.linearVelocity = moveDirection * MoveSpeed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }
    IEnumerator AnimationCurveRoutine()
    {
        float timePassed = 0f;

        Vector2 startPoint = transform.position;
        Vector2 EndPoint = new Vector2(transform.position.x + Random.Range(-1f,1f),transform.position.y + Random.Range(-1f,1f));

        while(timePassed < PopDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / PopDuration;

            float heighT = AnimationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, HeightY, heighT);

            transform.position = Vector2.Lerp(startPoint, EndPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }
    void DetectPickupType()
    {
        switch(pickupType)
        {
            case PickupType.GoldCoin:
                Debug.Log("Gold");
                break;
            case PickupType.HealthPickup:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickupType.StaminaPickup:
                Debug.Log("stamina");
                break;
        }
    }
}
