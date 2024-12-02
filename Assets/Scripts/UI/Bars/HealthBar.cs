using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public double currentHealth;
    private double changedHealth;

    public void SetMaxHealth(double health)
    {
        healthSlider.maxValue = (float)health;
        healthSlider.value = (float)health;

        currentHealth = health;
        changedHealth = health;
    }

    public void SetHealth(double health)
    {
        changedHealth = health;
    }

    private void Update()
    {
        if (healthSlider != null)
        {

            if (currentHealth > changedHealth)
            {
                double healthSmoothTake = (currentHealth - changedHealth) / 30 + 0.1 * Time.deltaTime;
                healthSlider.value -= (float)healthSmoothTake;
                currentHealth -= healthSmoothTake;

                if (healthSlider.value <= changedHealth)
                {
                    currentHealth = changedHealth;
                    healthSlider.value = (float)changedHealth;
                }
            }
            else if (currentHealth < changedHealth)
            {
                double healthSmoothAdd = (changedHealth - currentHealth) / 30 + 0.1 * Time.deltaTime;
                healthSlider.value += (float)healthSmoothAdd;
                currentHealth += healthSmoothAdd;

                if (healthSlider.value >= changedHealth)
                {
                    currentHealth = changedHealth;
                    healthSlider.value = (float)changedHealth;
                }
            }
            
        }
    }
}