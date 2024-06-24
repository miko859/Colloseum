using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject myPrefab;

    public PlayerController playerController;
    public Weapon[] weapons;

    public GameObject parentObject;
    public GameObject[] allChildren;

    private void Start()
        { 
        weapons = new Weapon[parentObject.transform.childCount];

        allChildren = new GameObject[parentObject.transform.childCount];

        for (int i = 0; i < allChildren.Length; i++)
        {
            allChildren[i] = parentObject.transform.GetChild(i).gameObject;
        }
        
        weapons[0] = allChildren[2].GetComponent<Weapon>();
        EquipWeapon(0);
    }

    public void EquipWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            playerController.EquipWeapon(weapons[index]);
        }
    }
}