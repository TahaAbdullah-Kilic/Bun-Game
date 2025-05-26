using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject SlashAnimationPrefab;
    [SerializeField] float AttackCooldown;
    [SerializeField] WeaponInfo WeaponInfo;
    Transform SlashAnimationSpawnPoint;
    GameObject WeaponColliderPrefab;
    Animator animator;
    GameObject slashAnimation;
    GameObject weaponCollider;
    float angle;
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.08f);
        Destroy(weaponCollider);
        StopCoroutine(Destroy());
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        WeaponColliderPrefab = PlayerController.Instance.GetWeaponCollider();
        SlashAnimationSpawnPoint = PlayerController.Instance.GetAttackAnimationSpawnPoint();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = PlayerController.Instance.transform.position;
        angle = SetAngle(mousePosition, playerPosition);
        MouseFollowWithOnset(mousePosition, playerPosition);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return WeaponInfo;
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        slashAnimation = Instantiate(SlashAnimationPrefab, SlashAnimationSpawnPoint.position, Quaternion.identity);
        weaponCollider = Instantiate(WeaponColliderPrefab, SlashAnimationSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
        weaponCollider.transform.parent = this.transform.parent;
        ActiveWeapon.Instance.AttackCooldown();
        StartCoroutine(Destroy());
    }
    private static float SetAngle(Vector3 mouseposition, Vector3 playerposition)
    {
        return Mathf.Atan2(mouseposition.y - playerposition.y, MathF.Abs(mouseposition.x - playerposition.x)) * Mathf.Rad2Deg;
    }
    void MouseFollowWithOnset(Vector3 mouseposition, Vector3 playerposition)
    {
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        if (mouseposition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            if (weaponCollider) weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            if (weaponCollider) weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void SwingDownSlashAnimation()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180,0,angle);
        if(!PlayerController.Instance.FacingRight) slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void SwingUpSlashAnimation()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (!PlayerController.Instance.FacingRight) slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
    }
}
