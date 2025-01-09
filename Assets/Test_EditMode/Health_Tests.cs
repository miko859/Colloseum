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
    [TestCase(20,80,true)]
    [TestCase(80,10,false)]
    public void DealDamage_ReducesHealthAndUpdatesHealth(float dmgValue, float expectedValue,Boolean expectedStatus)
    {
        health.DealDamage(dmgValue);
        health.healthBar.SetHealth(health.GetCurrentHealth());
        if (expectedStatus == false)
        {
            Assert.AreNotEqual(expectedValue, (float )health.GetCurrentHealth(), "Health did not reduce correctly.");
        }
        else { Assert.AreEqual(expectedValue, health.GetCurrentHealth(), "Health did not reduce correctly."); }
        //Assert.AreEqual(80, healthBar.healthSlider.value, "HealthBar slider value did not update correctly.");
    }

    [Test]
    //DMG,Heal,Expected,Status
    [TestCase(50,25,75, true)]
    [TestCase(50,5,75, false)]
    public void  DealDamageAndHeal_ReducesHealthUpdatesHealth(float dmgValue, float healValue, float expectedValue, Boolean expectedStatus)
    {
        
        health.DealDamage(dmgValue);
        health.Heal(healValue);
        //health.healthBar.SetHealth(health.GetCurrentHealth());
        if (expectedStatus == false)
        {
            Assert.AreNotEqual((double) expectedValue, health.GetCurrentHealth(), "Health did not reduce correctly.");
        }
        else { Assert.AreEqual(expectedValue, health.GetCurrentHealth(), "Health did not reduce correctly."); }
        //Assert.AreEqual(85, healthBar.healthSlider.value, "HealthBar slider value did not update correctly.");
    }

    [Test]
    [TestCase(10, 110, true)]
    public void Heal_UpdatesHealth(float healValue, float expectedValue, Boolean expectedStatus)
    {
        health.Heal(healValue);
        //health.healthBar.SetHealth(health.GetCurrentHealth());
        if (expectedStatus == false)
        {
            Assert.AreNotEqual(expectedValue, health.GetCurrentHealth(), "Health did not increase correctly.");
        }
        else { Assert.AreEqual(expectedValue, health.GetCurrentHealth(), "Health did not increase correctly."); }
        //Assert.AreEqual(150, healthBar.healthSlider.value, "HealthBar slider value exceeded max health.");
    }


    [TearDown]
    public void Teardown()
    {
        testObject = null;
        healthBar = null;
    }
}

