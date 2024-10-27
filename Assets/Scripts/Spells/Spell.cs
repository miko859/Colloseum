using UnityEngine;

/// <summary>
/// Abstract base class for all spells.
/// </summary>
public abstract class Spell : ManaSystem
{
    public int manaCost;
    protected ManaSystem manaSystem;

    protected virtual void Awake() 
    {
        manaSystem = FindObjectOfType<ManaSystem>();
        if (manaSystem == null)
        {
            Debug.LogError("ManaSystem not found in the scene!");
        }
    }

    public virtual void Activate()
    {
        if (manaSystem == null) 
        {
            Debug.LogError("ManaSystem is null in Activate.");
            return; 
        }

        if (manaSystem.TrySpendMana(manaCost))
        {
            Debug.Log($"{GetType().Name} spell activated.");
            StartManaSpending();
        }
        else
        {
            Debug.Log("Not enough mana!");
            StartCoroutine(manaSystem.VibrateManaBar(() => Debug.Log("Vibration Complete")));

        }
    }

    public virtual void Deactivate()
    {
        Debug.Log($"{GetType().Name} spell deactivated.");
    }
    protected virtual void StartManaSpending() { }
    protected virtual void StopManaSpending() { }
}
