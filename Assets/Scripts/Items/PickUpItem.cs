using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab; // Prefab zbrane, ktor� sa prid� do invent�ra

    public override void Interact()
    {
        Debug.Log("Picking up: " + weaponPrefab.name);
        EquipedWeaponManager.Instance.AddWeapon(weaponPrefab);
        //Destroy(gameObject); // Zni�� predmet po zdvihnut�
        weaponPrefab.transform.SetParent(GameObject.Find("Main Camera").transform, false);
        weaponPrefab.Unequip();
    }
}