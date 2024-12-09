using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class HealthEditModeTest
{
    private GameObject testObject;
    private Health healthComponent;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add required components
        testObject = new GameObject("TestObject");
        testObject.AddComponent<CapsuleCollider>(); // Adding a generic collider

        healthComponent = testObject.AddComponent<Health>();

        // Add and initialize required components for the Health script
        var healthBarObject = new GameObject("HealthBar");
        var healthBar = healthBarObject.AddComponent<HealthBar>();
        healthComponent.healthBar = healthBar;

        // Initialize the health component as if it started in the game
        healthComponent.maxHealth = 10;
        healthComponent.Start(); // Manually call Start to simulate component initialization
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(testObject);
    }

    [Test]
    public void HealthReducesOnDamage()
    {
        // Act
        healthComponent.DealDamage(5);

        // Assert
        Assert.AreEqual(5, healthComponent.CurrentHealth, "Health did not reduce correctly after taking damage.");
    }

    [Test]
    public void HealthIncreasesOnHeal()
    {
        // Arrange
        healthComponent.DealDamage(5);

        // Act
        healthComponent.Heal(3);

        // Assert
        Assert.AreEqual(8, healthComponent.CurrentHealth, "Health did not increase correctly after healing.");
    }

    [Test]
    public void HealthInitializesCorrectly()
    {
        // Assert initial health setup
        Assert.AreEqual(10, healthComponent.CurrentHealth, "Health did not initialize to max health correctly.");
    }

    [Test]
    public void EntityDiesWhenHealthReachesZero()
    {
        // Act
        healthComponent.DealDamage(10);

        // Assert
        Assert.AreEqual(0, healthComponent.CurrentHealth, "Health did not reach zero after taking lethal damage.");
        Assert.AreEqual("DeadEnemy", testObject.tag, "Tag was not set to 'DeadEnemy' upon death.");
    }
}
