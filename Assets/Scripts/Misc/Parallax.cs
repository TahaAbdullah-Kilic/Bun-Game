using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float ParallaxOffset = -0.1f;
    Camera cam;
    Vector2 startPosition;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        startPosition = transform.position;
    }
    void FixedUpdate()
    {
        if(cam != Camera.main) cam = Camera.main;
        transform.position = startPosition + travel * ParallaxOffset;
    }
}
