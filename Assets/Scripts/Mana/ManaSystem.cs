using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public Slider manaSlider;
    public int maxMana = 100;
    public int currentMana;
    public int fireballManaCost = 10;
    public int flamethrowerCost = 2;
    public int manaRegen = 1;
    public float regenInterval = 0.2f;
    private bool isFlameActive = false;
    private Coroutine manaSpendingCoroutine;
    private Vector3 originalPosition;
    public float vibrationAmount = 0.5f; 
    public float vibrationDuration = 0.5f;
    public float vibrationMagnitude = 7.1f;
    private bool isVibrating = false;
    void Start()
    {
        currentMana = maxMana;
        UpdateManaUI();
        StartCoroutine(RegenerateMana());
        originalPosition = manaSlider.transform.localPosition;
    }



    public IEnumerator VibrateManaBar(System.Action onVibrationComplete)
    {
        if (isVibrating) yield break;

        isVibrating = true;
        Vector3 originalPosition = manaSlider.transform.localPosition;

        float startTime = 0;
        while (startTime < vibrationDuration)
        {
            float xOffSet = Random.Range(-vibrationMagnitude, vibrationMagnitude);
            manaSlider.transform.localPosition = originalPosition + new Vector3(xOffSet, 0, 0);
            startTime += Time.deltaTime;
            yield return null;
        }

        manaSlider.transform.localPosition = originalPosition;
        onVibrationComplete?.Invoke();
        isVibrating = false;
    }

    
    public bool TrySpendMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            UpdateManaUI();
            return true;
        }
        return false;
    }

    
    public void StartSpendingManaForFlamethrower()
    {
        if (manaSpendingCoroutine == null && currentMana >= flamethrowerCost)
        {
            manaSpendingCoroutine = StartCoroutine(SpendManaPerSecond());
        }
    }

    public void StopSpendingManaForFlamethrower()
    {
        if (manaSpendingCoroutine != null)
        {
            StopCoroutine(manaSpendingCoroutine);
            manaSpendingCoroutine = null;
        }
    }

    private IEnumerator SpendManaPerSecond()
    {
        while (currentMana >= flamethrowerCost)
        {
            yield return new WaitForSeconds(1f);
            currentMana -= flamethrowerCost;
            UpdateManaUI();
        }
        StopSpendingManaForFlamethrower();  // Stop if out of mana
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

    void UpdateManaUI()
    {
        manaSlider.value = currentMana;
    }
}
