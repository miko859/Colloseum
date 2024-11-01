using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string target;
    private bool isEnemyWeapon;
    private Collider blade;
    private bool hit = false;
    private WeaponAnimations weaponAnimations;
    private AIController aiController;
    private Transform owner;

    /// <summary>
    /// Set blade for hitboxes and weaponAnimations to get DMG of actual attack
    /// </summary>
    void Start()
    {
        blade = GetComponent<Collider>();
        

        owner = FindObjectWithTag(transform, "Player") ?? FindObjectWithTag(transform, "Enemy");


        /// Check who is owner of weapon, if nobody than target will be Enemy, cause enemy will not be picking a weapon
        if (owner != null)
        {
            if (owner.CompareTag("Player"))
            {
                target = "Enemy";
                weaponAnimations = GetComponent<WeaponAnimations>();
                isEnemyWeapon = false;
            }
            else if (owner.CompareTag("Enemy"))
            {
                target = "Player";
                aiController = owner.GetComponent<AIController>(); 
                isEnemyWeapon = true;
            }
        }
        else
        {
            target = "Enemy";
        }
    }

    /// <summary>
    /// Check if entity was hit by weapon and dealing dmg by attack, enemy <=> player
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target) & !hit)
        {
            other.GetComponent<Health>().DealDamage(CalculateDamage());
            Debug.Log("you hit " + target + " by damage " + CalculateDamage());
            hit = true; 

            if (other.tag == "Player")
            {
                other.transform.GetChild(0).GetComponent<PlayerController>().GotHit();
            }
        }
    }

    public void SetHit(bool hit)
    {
        this.hit = hit;
    }

    private Transform FindObjectWithTag(Transform obj, string tag)
    {
        Transform current = obj;

        while (current != null)
        {
            if (current.CompareTag(tag))
            {
                return current;
            }
            current = current.parent;
        }

        return current;
    }

    private float CalculateDamage()
    {
        if (isEnemyWeapon && aiController != null)
        {
            return aiController.getDamage();
        }
        else
        {
            return weaponAnimations.getDamage();
        }
    }
}
