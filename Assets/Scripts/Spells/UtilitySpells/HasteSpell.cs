using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for haste spells.
/// </summary>
public class HasteSpell : Spell
{
    public ManaSystem manaSystem;
    private float duration = 5f; 
    private float speedMultiplier = 1.5f;
    private bool isOnCooldown = false; // Prevents activation during cooldown so that the spell cant stack 

    public override void Initialize()
    {
        manaCost = 20;
    }

    public override void Activate()
    {
        if (isOnCooldown)
        {
            Debug.Log("Haste spell is on cooldown. Please wait.");
            return;
        }

        base.Activate();

        if (manaSystem == null)
        {
            Debug.LogError("ManaSystem is null in HasteSpell. Cannot activate the spell.");
            return;
        }

        if (manaSystem.TrySpendMana(manaCost))
        {
            StartCoroutine(HandleHasteEffect());
        }
    }

    private IEnumerator HandleHasteEffect()
    {
        isOnCooldown = true; 
        Debug.Log("Haste spell activated!");

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.base_move_speed *= speedMultiplier; 
            yield return new WaitForSeconds(duration); 
            playerMovement.base_move_speed /= speedMultiplier; 
        }
        else
        {
            Debug.LogError("PlayerMovement script not found on player.");
        }

        Debug.Log("Haste spell effect ended.");
        isOnCooldown = false; 
        Debug.Log("Haste spell is ready to be activated again.");

        Deactivate();
    }
}
