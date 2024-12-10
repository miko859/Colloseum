using System;
using System.Collections;
using System.Drawing.Printing;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndHealthBarTests
{
    private GameObject testObject;
    private Health health;
    private HealthBar healthBar;

    [SetUp]
    public void Setup()
    {
        // Create a GameObject and add necessary components
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
        health.Start();
    }

    [Test]
    public void DealDamage_ReducesHealthAndUpdatesHealthBar()
    {
        health.DealDamage(20);
        Assert.AreEqual(80, health.GetCurrentHealth(), "Health did not reduce correctly.");
        Assert.AreEqual(100, healthBar.healthSlider.value, "HealthBar slider value did not update correctly.");
    }

    [Test]
    public void  Heal_IncreasesHealthAndUpdatesHealthBar()
    {
        
        health.DealDamage(50);
        //Thread.Sleep(100); // Pause for 100 milliseconds
        health.Heal(30);
        Assert.AreEqual(80, health.GetCurrentHealth(), "Health did not increase correctly.");
        Assert.AreEqual(100, healthBar.healthSlider.value, "HealthBar slider value did not update correctly.");
    }

    [Test]
    public void HealthDoesNotExceedMaxHealth()
    {
        health.Heal(50);
        Assert.AreEqual(150, health.GetCurrentHealth(), "");
        Assert.AreEqual(100, healthBar.healthSlider.value, "HealthBar slider value exceeded max health.");
    }


    [TearDown]
    public void Teardown()
    {
        testObject = null;
        healthBar = null;
    }
}

