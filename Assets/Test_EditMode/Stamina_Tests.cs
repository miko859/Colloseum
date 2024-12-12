using System;
using System.Collections;
using System.Drawing.Printing;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_Tests
{
    private GameObject testObject;
    private StaminaBar stamina;
    private StaminaBar staminaBar;

    [SetUp]
    public void Setup()
    {
        // Neccesary Components
        testObject = new GameObject();
        testObject.AddComponent<CapsuleCollider>();
        stamina= testObject.AddComponent<StaminaBar>();
        GameObject healthBarObject = new GameObject();
        staminaBar = testObject.AddComponent<StaminaBar>();
        Slider staminaSliderBar = healthBarObject.AddComponent<Slider>();

        // Link the Health and HealthBar components
        stamina.staminaSlider = staminaSliderBar;

        // Initialize the components
        stamina.SetMaxStamina(100,2);
        
    }

    [Test]
    public void ReduceStamina_ReducesStamina()
    {
        stamina.ReduceStamina(22.1);
        Assert.AreEqual(77.9, stamina.GetCurrentStamina(),"Staminda did not reduce correctly");
       
    }
     [Test]
    public void AddStamina_StaminaCanBeAddedAfterBeingReduces()
    {
        stamina.ReduceStamina(22.1);
        Assert.AreEqual(77.9, stamina.GetCurrentStamina(),"Staminda did not reduce correctly");

        stamina.AddStamina(8);
        Assert.AreEqual(85.9, stamina.GetCurrentStamina(),"Staminda did not add correctly");

    }


    [TearDown]
    public void Teardown()
    {
        testObject = null;
        staminaBar = null;
    }
}

