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
    //   Value A, Value B, Expected to Pass true/false
    [Test]
    [TestCase(22.1,77.9,true)]
    [TestCase(10,90,true)]
    [TestCase(90,10,true)]
    [TestCase(10.1,77.9,false)]
    [TestCase(0,90,false)]
    [TestCase(int.MaxValue,int.MinValue,false)]
    public void ReduceStamina_ReducesStamina(double valueA, double valueB,Boolean pass)
    {   //Reduces Stamina with value A
        stamina.ReduceStamina(valueA);

        //If testcase is setup to fail
        if (pass == false)
        {Assert.AreNotEqual(valueB, stamina.GetCurrentStamina(),"Staminda did not reduce correctly");}
        
        //If its expected to pass
        else{Assert.AreEqual(valueB, stamina.GetCurrentStamina(),"Staminda did not reduce correctly");
       }
    }
     [Test]
     [TestCase(20,80,true,5,85,true)]
     [TestCase(95,5,true,5,10,true)]
     [TestCase(5,100,false,19,100,false)]
    public void AddStamina_StaminaCanBeAddedAfterBeingReduces(double Reduce, double ExpectedValueA,Boolean ExpectedStatus,double Add,double ExpectedValueB,Boolean ExpectedStatus2)
    {
        //Reduces Stamina
        stamina.ReduceStamina(Reduce);

        //Check if the test it expected to pass
        if(ExpectedStatus == false){
            Assert.AreNotEqual(ExpectedValueA, stamina.GetCurrentStamina(),"Staminda did not reduce correctly");
        }
        else{
            Assert.AreEqual(ExpectedValueA, stamina.GetCurrentStamina(),"Staminda did not reduce correctly");
        }
        
        //Reduces Stamina
        stamina.AddStamina(Add);

         //Check if the test it expected to pass
        if (ExpectedStatus2 == false){
            Assert.AreNotEqual(ExpectedValueB, stamina.GetCurrentStamina(),"Staminda did not add correctly");
        }
        else {
            Assert.AreEqual(ExpectedValueB, stamina.GetCurrentStamina(),"Staminda did not add correctly");
        }

    }


    [TearDown]
    public void Teardown()
    {
        testObject = null;
        staminaBar = null;
    }
}

