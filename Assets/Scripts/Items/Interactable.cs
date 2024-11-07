using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactPrompt = "Press E to interact";
    /// <summary>
    /// Function, triggered by interaction by player with items
    /// </summary>
    public abstract void Interact();
    public abstract void EquipWeapon(Item item);


    /// <summary>
    /// To show on screen it's interactable and what to press
    /// </summary>
    /// <returns></returns>
    public virtual string GetInteractPrompt()
    {
        return interactPrompt;
    }
}