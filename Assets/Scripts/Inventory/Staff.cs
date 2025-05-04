using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfo WeaponInfo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollowWithOnset();
    }
    public void Attack()
    {
        Debug.Log("Staff Attack");
        ActiveWeapon.Instance.AttackCooldown();
    }
    public WeaponInfo GetWeaponInfo()
    {
        return WeaponInfo;
    }
    void MouseFollowWithOnset()
    {
        Vector3 mousePosition = Input.mousePosition;
        //Vector3 playerPosition = PlayerController.Instance.transform.position;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        //float angle = Mathf.Atan2(mousePosition.y - playerPosition.y, MathF.Abs(mousePosition.x - playerPosition.x)) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(mousePosition.y,mousePosition.x) * Mathf.Rad2Deg;
        if (mousePosition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}
