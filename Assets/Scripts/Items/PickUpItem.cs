using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab; // Prefab zbrane, ktorá sa pridá do inventára

    public override void Interact()
    {
        Debug.Log("Picking up: " + weaponPrefab.name);
        EquipedWeaponManager.Instance.AddWeapon(weaponPrefab);
        //Destroy(gameObject); // Znièí predmet po zdvihnutí
        weaponPrefab.transform.SetParent(GameObject.Find("Main Camera").transform, false);
        weaponPrefab.Unequip();
    }
}