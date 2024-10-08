using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public Slider manaSlider;
    public int maxMana = 100;
    public int currentMana;
    public int fireballManaCost = 10;
    public int manaRegen = 1;
    public float regenInterval = 0.2f;

    void Start()
    {
        currentMana = maxMana;
        UpdateManaUI();

        StartCoroutine(RegenerateMana());
    }
    IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);

            if (currentMana < maxMana)
            {
                currentMana = Mathf.Min(currentMana + manaRegen, maxMana); 
                UpdateManaUI();
            }
        }
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
