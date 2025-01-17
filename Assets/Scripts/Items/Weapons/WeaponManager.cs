using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string target;
    private bool isEnemyWeapon;
    public Collider blade;
    public Collider bashColl;
    private bool hit = false;
    private WeaponAnimations weaponAnimations;
    private AIController aiController;
    private Transform owner;
    private bool isBashing = false;

    /// <summary>
    /// Set blade for hitboxes and weaponAnimations to get DMG of actual attack
    /// </summary>
    void Start()
    {
        if (blade == null)
        {
            blade = GetComponent<Collider>();
        }

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
        if (target.Equals("Enemy") && bashColl.enabled)
        {  
            if (owner.transform.GetChild(0).GetComponent<PlayerController>().GetIsBashing())
            {
                Vector3 direction = new Vector3(other.transform.position.x - transform.position.x,
                                                0,
                                                other.transform.position.z - transform.position.z);
                

                AIController enemy = other.transform.GetComponent<AIController>();
                StartCoroutine(enemy.BeingPushedMovement(1.5f, direction));
            }
        }
        else if (other.CompareTag(target) && !hit && blade.isTrigger)
        {
            other.GetComponent<Health>().DealDamage(CalculateDamage(other));
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

    private float CalculateDamage(Collider other)
    {
        if (isEnemyWeapon && aiController != null)
        {
            PlayerController playerController = other.transform.GetChild(0).GetComponent<PlayerController>();
            WeaponAnimations playerWeaponAnimations = other.GetComponentInChildren<WeaponAnimations>();
            Debug.Log(aiController.getDamage());
            Debug.Log(DifficultyManager.Instance.GetEnemyDamageMultiplier());
            float totalIncomingDmg = aiController.getDamage() * DifficultyManager.Instance.GetEnemyDamageMultiplier();

            if (playerWeaponAnimations.getIsBlocking() && playerController.GetStaminaBar().GetCurrentStamina() > playerWeaponAnimations.weaponData.blockStaminaCons)
            {
                playerController.GetStaminaBar().ReduceStamina(playerWeaponAnimations.weaponData.blockStaminaCons);
                float total = totalIncomingDmg - playerWeaponAnimations.weaponData.blockTreshhold;
                playerWeaponAnimations.BlockedAttack();
                Debug.Log($"total damage {total} AI dmg {aiController.getDamage()} player block {playerWeaponAnimations.weaponData.blockTreshhold}");
                if (total > 0)
                {
                    return total;
                }
                return 0;
            }
            return totalIncomingDmg;
        }
        else
        { 
            return weaponAnimations.getDamage();
        }
    }

    public IEnumerator SwapCollBlockBash()
    {
        blade.enabled = false;
        bashColl.enabled = true;
        yield return new WaitForSeconds(weaponAnimations.weaponData.bashDuration);
        blade.enabled = true;
        bashColl.enabled = false;
    }
}
