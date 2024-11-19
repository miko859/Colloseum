using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab; // Prefab of weapon that adds to inventory
    public bool destroyObject;
    private string posInHierarchyOfWeapon = "Body"; 

    public static PickUpItem Instance;

    public override void Interact()
    {
        InventoryManager.Instance.Add(weaponPrefab.weaponData);

        if (destroyObject)
        {
            Destroy(transform.gameObject);
        }
    }

    public void Awake()
    {
        Instance = this;
    }

    public override void EquipWeapon(Item item) 
    {
        if (item is WeaponData weaponData)
        {
            Weapon temp = Instantiate(weaponData.weaponPrefab);
            Debug.Log("Added weapon: " + temp);
            temp.name = weaponData.name;
            temp.Unequip();
            temp.transform.SetParent(GameObject.Find(posInHierarchyOfWeapon).transform, false);
            EquipedWeaponManager.Instance.AddWeapon(temp);
        }
    }
}
