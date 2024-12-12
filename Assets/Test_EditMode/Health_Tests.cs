using System;
using System.Collections;
using System.Drawing.Printing;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Health_Tests
{
    private GameObject testObject;
    private Health health;
    private HealthBar healthBar;

    [SetUp]
    public void Setup()
    {
        // Neccesary Components
        testObject = new GameObject();
        testObject.AddComponent<CapsuleCollider>();
        health = testObject.AddComponent<Health>();
        GameObject healthBarObject = new GameObject();
        healthBar = healthBarObject.AddComponent<HealthBar>();
        Slider slider = healthBarObject.AddComponent<Slider>();
        healthBar.healthSlider = slider;

        // Link the Health and HealthBar components
        health.healthBar = healthBar;

        // Initialize the components
        health.maxHealth = 100;
        health.SetCurrentHealth(health.maxHealth);
        //health.healthBar.SetHealth(health.GetCurrentHealth());
        health.Start();
    }

    [Test]
    public void DealDamage_ReducesHealthAndUpdatesHealth()
    {
        health.DealDamage(20);
        health.healthBar.SetHealth(health.GetCurrentHealth());
        Assert.AreEqual(80, health.GetCurrentHealth(), "Health did not reduce correctly.");
        //Assert.AreEqual(80, healthBar.healthSlider.value, "HealthBar slider value did not update correctly.");
    }

    [Test]
    public void  DealDamageAndHeal_ReducesHealthUpdatesHealth()
    {
        
        health.DealDamage(50);
        health.Heal(35);
        //health.healthBar.SetHealth(health.GetCurrentHealth());
        Assert.AreEqual(85, health.GetCurrentHealth(), "Health did not increase correctly.");
        //Assert.AreEqual(85, healthBar.healthSlider.value, "HealthBar slider value did not update correctly.");
    }

    [Test]
    public void Heal_UpdatesHealth()
    {
        health.Heal(50);
        //health.healthBar.SetHealth(health.GetCurrentHealth());
        Assert.AreEqual(150, health.GetCurrentHealth(), "");
        //Assert.AreEqual(150, healthBar.healthSlider.value, "HealthBar slider value exceeded max health.");
    }


    [TearDown]
    public void Teardown()
    {
        testObject = null;
        healthBar = null;
    }
}

