using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;

    private double currentStamina;
    private double maxStamina;
    private float regenTimer;
    private float baseStaminaRegen;
    private float otherFactorsStaminaRegen;

    public void SetMaxStamina(double stamina, double baseStaminaRegen)
    {
        staminaSlider.maxValue = (float)stamina;
        staminaSlider.value = (float)stamina;

        currentStamina = stamina;
        maxStamina = stamina;

        this.baseStaminaRegen = (float)baseStaminaRegen;
    }

    public float GetStaminaRegen()
    {
        return baseStaminaRegen + otherFactorsStaminaRegen;
    }

    public double GetCurrentStamina()
    {
        return currentStamina;
    }

    public void AddStamina(double stamina)
    {
        currentStamina += stamina;
    }

    public void ReduceStamina(double stamina)
    {
        currentStamina -= stamina;
    }

    void Update()
    {


        if (staminaSlider != null)
        {
            StaminaRegen();
            if (currentStamina > staminaSlider.value)
            {
                staminaSlider.value += GetStaminaRegen() * Time.deltaTime;

                if (staminaSlider.value > currentStamina)
                {
                    staminaSlider.value = (float)currentStamina;
                }
            }
            if (currentStamina < staminaSlider.value)
            {
                staminaSlider.value -= (float)((staminaSlider.value - currentStamina) / 8 + 0.1 * Time.deltaTime);
            
                if (staminaSlider.value < currentStamina)
                {
                    staminaSlider.value = (float)currentStamina;
                }
            }
        }
    }

    protected void StaminaRegen()
    {
        
            regenTimer += Time.deltaTime;
            if (regenTimer >= 1)
            {
                if (currentStamina + GetStaminaRegen() > maxStamina)
                {
                    currentStamina = maxStamina;
                }
                else
                {
                    currentStamina += GetStaminaRegen();
                }
                regenTimer = 0;
            }
        
    }

}
