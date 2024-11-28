using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Abstract base class for all spells.
/// </summary>
public abstract class Spell : MonoBehaviour
{
    public int manaCost;
    protected ManaSystem manaSystem;

    private void Awake() 
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
            Debug.LogError($"ManaSystem is null in Activate. Spell: {gameObject.name}, Instance ID: {GetInstanceID()}");
            return;
        }

        if (this is not Element && !manaSystem.TrySpendMana(manaCost))
        {
            Debug.Log("Not enough mana!");
            StartCoroutine(manaSystem.VibrateManaBar(() => Debug.Log("Vibration Complete")));
        }
        else
        {
            Debug.Log($"{GetType().Name} spell activated.");
            StartManaSpending();
        }
    }

    public virtual void StartManaSpending() { }
    public virtual void ActiveBall()
    {
        
    }

    public virtual void Deactivate()
    {
        Debug.Log($"{GetType().Name} spell deactivated.");
    }

    public virtual void Initialize() { }
}
