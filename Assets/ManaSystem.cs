using System.Collections;
using Unity.VisualScripting;
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
    public float vibrationDuration = 0.5f;
    public float vibrationMagnitude = 7.1f;
    private bool isVibrating = false;
    private FlameThrower flameThrower;

    private Coroutine manaSpendingCoroutine;

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

    public Coroutine StartSpendingManaPerSecond()
    {
        if (manaSpendingCoroutine == null)
        {
            manaSpendingCoroutine = StartCoroutine(SpendManaPerSecond());
        }
        return manaSpendingCoroutine;
    }

    public void StopSpendingManaPerSecond()
    {
        if (manaSpendingCoroutine != null)
        {
            StopCoroutine(manaSpendingCoroutine);
            manaSpendingCoroutine = null; // Clear the reference after stopping
        }
    }

    public IEnumerator SpendManaPerSecond()
    {
        bool vibrationIsTriggered = false;

        while (currentMana >= flamethrowerCost)
        {
            Debug.Log("Spending mana...");
            yield return new WaitForSeconds(0.2f);

            currentMana -= flamethrowerCost;
            UpdateManaUI();

            if (currentMana < flamethrowerCost)
            {
                Debug.Log("Not enough mana!");
                vibrationIsTriggered = true;
                StartCoroutine(VibrateManaBar(() => vibrationIsTriggered = false));
                break;
            }
        }

        StopSpendingManaPerSecond(); // Ensure it stops when mana is insufficient
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
            StartCoroutine(VibrateManaBar(() => { isVibrating = false; }));
        }

        return false;
    }

    IEnumerator VibrateManaBar(System.Action onVibrationComplete)
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

    void UpdateManaUI()
    {
        manaSlider.value = currentMana;
    }
}
