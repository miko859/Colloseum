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
    public float vibrationDuration = 0.5f;
    public float vibrationMagnitude = 7.1f;
    private bool isVibrating = false;

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
        if (!isVibrating)
        {
            StartCoroutine(VibrateManaBar());

        }
        return false; 
    }

   

    IEnumerator VibrateManaBar()
    {
        isVibrating = true;
        Vector3 originalPosition = manaSlider.transform.localPosition;

        float startTime = 0;
        while (startTime < vibrationDuration)
        {
            float xOffSet = Random.Range(0, vibrationMagnitude);
            manaSlider.transform.localPosition = originalPosition + new Vector3(xOffSet, 0, 0);
            startTime += Time.deltaTime;
            yield return null;
        }
        manaSlider.transform.localPosition = originalPosition;
        isVibrating = false;
    }
    void UpdateManaUI()
    {
        // mana slider change/update
        manaSlider.value = currentMana;
    }
}
