using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public Slider manaSlider;
    public int maxMana = 100;
    public int currentMana;
    public int fireballManaCost = 10;

    void Start()
    {
        currentMana = maxMana;
        UpdateManaUI();
    }

    public bool SpendMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            UpdateManaUI();
            return true; 
        }
        return false; 
    }

    void UpdateManaUI()
    {
        // mana slider change/update
        manaSlider.value = currentMana;
    }
}
