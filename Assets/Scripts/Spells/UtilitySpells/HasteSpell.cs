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





    public override void Initialize()
    {
        manaCost = 20;
    }

    public override void Activate()
    {
        base.Activate();
        if (manaSystem == null)
        {
            Debug.LogError("ManaSystem is null in HasteSpell. Cannot activate the spell.");
            return;
        }
        if (manaSystem.TrySpendMana(manaCost))
        {
            StartCoroutine(IncreaseSpeed());
        }
    }

    private IEnumerator IncreaseSpeed()
    {
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
        Deactivate();
    }
}
