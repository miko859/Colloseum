using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab;     // Prefab of weapon that adds to inventory
    public bool destroyObject;
    private string posInHierarchyOfWeapon = "Body";     // Change this, if you want new save pos for weapon in hierarchy
    public Item item;

    public static PickUpItem Instance;
    public override void Interact()
    {
        
       /* Weapon temp = Instantiate(weaponPrefab);
        temp.name = weaponPrefab.name;
        temp.Unequip();
        temp.transform.SetParent(GameObject.Find(posInHierarchyOfWeapon).transform, false);  
        EquipedWeaponManager.Instance.AddWeapon(temp);*/
        InventoryManager.Instance.Add(item);

        if (destroyObject)
        {
            Destroy(transform.gameObject);
        }

    }

    public void Awake()
    {
        Instance = this;
    }

    public override void EquipWeapon()
    {
        Weapon temp = Instantiate(weaponPrefab);
        temp.name = weaponPrefab.name;
        temp.Unequip();
        temp.transform.SetParent(GameObject.Find(posInHierarchyOfWeapon).transform, false);
        EquipedWeaponManager.Instance.AddWeapon(temp);
    }
}