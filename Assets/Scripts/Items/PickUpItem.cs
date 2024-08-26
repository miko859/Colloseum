using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab; // Prefab zbrane, ktorá sa pridá do inventára

    public override void Interact()
    {
        Debug.Log("Picking up: " + weaponPrefab.name);
        EquipedWeaponManager.Instance.AddWeapon(weaponPrefab); // Predpokladá sa, že WeaponManager spravuje zbrane
        Destroy(gameObject); // Znièí predmet po zdvihnutí
    }
}