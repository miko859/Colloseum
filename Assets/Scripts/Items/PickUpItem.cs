using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab;     // Prefab of weapon that adds to inventory
    public bool destroyObject;      

    public override void Interact()
    {
        
        Weapon temp = Instantiate(weaponPrefab);
        temp.name = weaponPrefab.name;
        temp.Unequip();
        temp.transform.SetParent(GameObject.Find("Main Camera").transform, false);
        EquipedWeaponManager.Instance.AddWeapon(temp);

        if (destroyObject)
        {
            Destroy(transform.gameObject);
        }

    }
}