using UnityEngine;

public class PickUpItem : Interactable
{
    public Weapon weaponPrefab;     // Prefab of weapon that adds to inventory
    public bool destroyObject;
    private string posInHierarchyOfWeapon = "Body"; // Change this if you want a new save position for the weapon in hierarchy

    public override void Interact()
    {
        
        Weapon temp = Instantiate(weaponPrefab);
        temp.name = weaponPrefab.name;
        temp.Unequip();
        temp.transform.SetParent(GameObject.Find(posInHierarchyOfWeapon).transform, false);

        // Add weapon to equipped weapon manager
        EquipedWeaponManager.Instance.AddWeapon(temp);

        // Add weapon to inventory manager
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.AddWeaponToInventory(temp);
        }
        else
        {
            Debug.LogWarning("InventoryManager not found in the scene!");
        }

        
        if (destroyObject)
        {
            Destroy(transform.gameObject);
        }
    }
}
