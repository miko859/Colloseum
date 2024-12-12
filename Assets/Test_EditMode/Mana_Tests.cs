using System;
using System.Collections;
using System.Drawing.Printing;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Mana_Tests
{
    private GameObject testObject;
    private ManaSystem mana;    
    private Slider manaBar;

    [SetUp]
    public void Setup()
    {
        // Neccesary Components
        testObject = new GameObject();
        testObject.AddComponent<CapsuleCollider>();
        mana = testObject.AddComponent<ManaSystem>();
        GameObject manaBarObject = new GameObject();
        //manaBar = manaBarObject.AddComponent<ManaSystem>();
        Slider manaslider = manaBarObject.AddComponent<Slider>();

        // Link the ManaSystem and ManaBar components
        mana.manaSlider = manaslider;

        // Initialize the components
        mana.maxMana = 100f;
        mana.currentMana = mana.maxMana;
        mana.fireballManaCost =10f;
        mana.flamethrowerCost = 2.5f;

        
       
    }

    [Test]
    public void ManaDrain_ValueUpdatesAfter_Fireball()
    {
        mana.TrySpendMana(mana.fireballManaCost);
        Assert.AreEqual(90,mana.currentMana,"Mana did not reduce correctly.");
    }

     [Test]
    public void ManaDrain_ValueUpdatesAfter_FlameThrower()
    {
    
        //Simulates Holding the FLamethrower spell    
        for (int i = 0; i < 10; i++){
            mana.TrySpendMana(mana.flamethrowerCost);
        }
        Assert.AreEqual(75,mana.currentMana,"Mana did not reduce correctly.");


    }
      [Test]
    public void ManaDrain_ValueUpdatesAfter_FlameThrowerAndFireball()
    {
        mana.TrySpendMana(mana.fireballManaCost);
        Assert.AreEqual(90,mana.currentMana,"Mana did not reduce correctly.");

        //Simulates Holding the FLamethrower spell    
        for (int i = 0; i < 10; i++){
            mana.TrySpendMana(mana.flamethrowerCost);
        }
        Assert.AreEqual(65,mana.currentMana,"Mana did not reduce correctly.");

    }


    [TearDown]
    public void Teardown()
    {
        testObject = null;
        mana = null;
    }
}

