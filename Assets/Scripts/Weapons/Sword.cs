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
        MouseFollowWithOnset();
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
    void MouseFollowWithOnset()
    {
        Vector3 mousePosition = Input.mousePosition;
        //Vector3 playerPosition = PlayerController.Instance.transform.position;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        //float angle = Mathf.Atan2(mousePosition.y - playerPosition.y, MathF.Abs(mousePosition.x - playerPosition.x)) * Mathf.Rad2Deg;
        if(mousePosition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,0);
            if(weaponCollider) weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0); 
            if(weaponCollider) weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        } 
    }
    public void SwingDownSlashAnimation()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180,0,0);
        if(!PlayerController.Instance.FacingRight) slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void SwingUpSlashAnimation()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (!PlayerController.Instance.FacingRight) slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
    }
}
