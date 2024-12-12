using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public static Equip Instance;

  
    private string posInHierarchyOfWeapon = "Body";

    private void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon()
    {
        /*Weapon temp = Instantiate(weaponPrefab);
        temp.name = weaponPrefab.name;
        temp.Unequip();
        temp.transform.SetParent(GameObject.Find(posInHierarchyOfWeapon).transform, false);
        EquipedWeaponManager.Instance.AddWeapon(temp);*/
    }

}
