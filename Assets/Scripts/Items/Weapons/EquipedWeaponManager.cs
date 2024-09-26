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

    public RigBuilder rigBUilder;

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
            
            if (currentWeaponIndex != -1)
            {
                weaponery[currentWeaponIndex].Unequip();
            }

            rigController.Play("unequip_" + weaponery[currentWeaponIndex].weaponData.weaponName);
            rigBUilder.Clear();
            currentWeaponIndex = index;
            weaponery[currentWeaponIndex].Equip();

            transform.GetComponent<PlayerController>().EquipWeapon(weaponery[currentWeaponIndex]);

            //rigController.Play("equip_" + weaponery[currentWeaponIndex].weaponData.weaponName);
            rigController.SetBool("equip_" +  weaponery[currentWeaponIndex].weaponData.weaponName, true);

            Debug.Log("Right Grip -> " + weaponery[currentWeaponIndex].transform.Find("RightGrip"));
            Debug.Log("Left Grip -> " + weaponery[currentWeaponIndex].transform.Find("LeftGrip"));

            //rightHandGrip.GetComponent<TwoBoneIKConstraint>().enabled = false;
            //leftHandGrip.GetComponent<TwoBoneIKConstraint>().enabled = false;

            rightHandGrip.GetComponent<TwoBoneIKConstraint>().data.target = weaponery[currentWeaponIndex].transform.Find("RightGrip");
            leftHandGrip.GetComponent<TwoBoneIKConstraint>().data.target = weaponery[currentWeaponIndex].transform.Find("LeftGrip");

            
            //rigBUilder.Clear();
            rigBUilder.Build();

            
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