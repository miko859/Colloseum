using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerController playerController;
    public Weapon[] weapons;

    /// <summary>
    /// This will equip weapon from inventory based on index recevied from InputSystem
    /// </summary>
    /// <param name="index"></param>
    public void EquipWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            playerController.EquipWeapon(weapons[index]);
        }
    }
}