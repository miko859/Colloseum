using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EquipedWeaponManager : MonoBehaviour
{
    public static EquipedWeaponManager Instance;

    public Transform rightHandGrip;
    public Transform leftHandGrip;
    public Animator rigController;

    public Rig rightRig;
    public Rig leftRig;

    public List<Weapon> weaponery = new List<Weapon>();
    private int currentWeaponIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int getCurrentWeaponIndex() 
    {  
        return currentWeaponIndex; 
    }

    /// <summary>
    /// This will equip weapon from inventory based on index recevied from InputSystem
    /// </summary>
    /// <param name="index"></param>
    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weaponery.Count)
        {
            // Deaktivuj súèasnú zbraò
            if (currentWeaponIndex != -1)
            {
                weaponery[currentWeaponIndex].Unequip();
            }

            // Aktivuj novú zbraò
            currentWeaponIndex = index;
            weaponery[currentWeaponIndex].Equip();

            transform.GetComponent<PlayerController>().EquipWeapon(weaponery[currentWeaponIndex]);

            rigController.Play("equip_" + weaponery[currentWeaponIndex].weaponData.weaponName);

            Debug.Log("Right Grip -> " + weaponery[currentWeaponIndex].transform.Find("RightGrip"));
            Debug.Log("Left Grip -> " + weaponery[currentWeaponIndex].transform.Find("LeftGrip"));

            //rightHandGrip.GetComponent<TwoBoneIKConstraint>().enabled = false;
            //leftHandGrip.GetComponent<TwoBoneIKConstraint>().enabled = false;

            rightHandGrip.GetComponent<TwoBoneIKConstraint>().data.target = weaponery[currentWeaponIndex].transform.Find("RightGrip");
            leftHandGrip.GetComponent<TwoBoneIKConstraint>().data.target = weaponery[currentWeaponIndex].transform.Find("LeftGrip");
        }
    }

    
    /// <summary>
    /// Add weapon into weaponery/inventory
    /// </summary>
    /// <param name="newWeapon"></param>
    public void AddWeapon(Weapon newWeapon)
    {
        weaponery.Add(newWeapon);
        
        Debug.Log("Weapon added: " + newWeapon.name);
    }
}