using System;
using UnityEngine;

public class ActiveInventorySlot : Singleton<ActiveInventorySlot>
{
    int activeInventorySlotIndex;
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown((KeyCode)(48 + i)))
            {
                ToggleActiveSlot(i);
            }
        }
    }
    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }
    void ToggleActiveSlot(int IndexNumber)
    {
        ToggleActiveHighlight(IndexNumber-1);
    }
    void ToggleActiveHighlight(int IndexValue)
    {
        activeInventorySlotIndex = IndexValue;
        foreach (Transform activeInventorySlot in transform)
        {
            activeInventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        transform.GetChild(IndexValue).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    void ChangeActiveWeapon()
    {
        InventorySlot inventorySlot = transform.GetChild(activeInventorySlotIndex).GetComponentInChildren<InventorySlot>();
        if(PlayerHealth.Instance.IsDead) return;

        if(ActiveWeapon.Instance.CurrentActiveWeapon != null) Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);

        if(inventorySlot.GetWeaponInfo() == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }
        GameObject weaponToSpawn = inventorySlot.GetWeaponInfo().weaponPrefab;
        
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.SetParent(ActiveWeapon.Instance.transform);
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
