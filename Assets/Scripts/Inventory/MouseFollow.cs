using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    void Update()
    {
        FaceMouse();
    }
    void FaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        transform.right = direction;
    }
}
