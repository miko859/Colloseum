using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using System.Collections;

public class HealthTests : MonoBehaviour {
    private GameObject testObject;
    private Health health;
    private HealthBar healthBar;
    private WeaponManager weaponManager;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        health = testObject.AddComponent<Health>();

        // Adding necessary components
        healthBar = testObject.AddComponent<HealthBar>();
        health.healthBar = healthBar;

        testObject.AddComponent<Animator>();
        testObject.AddComponent<CapsuleCollider>();
        testObject.AddComponent<Weapon>();

        // Create and initialize the WeaponManager
        var weaponManagerObject = new GameObject();
        weaponManagerObject.transform.parent = testObject.transform;
        weaponManager = weaponManagerObject.AddComponent<WeaponManager>();
        health.weaponManager = weaponManager;

        // Initialize the WeaponManager's colliders
        weaponManager.blade = weaponManagerObject.AddComponent<CapsuleCollider>();
        weaponManager.bashColl = weaponManagerObject.AddComponent<BoxCollider>();

        // Initializing the health class
        health.Start();
    }

    [Test]
    public void Start_InitializesHealthCorrectly()
    {
        Assert.AreEqual(health.maxHealth, health.CurrentHealth);
        Assert.AreEqual(health.maxHealth, health.healthBar.MaxHealth);
    }

    [UnityTest]
    public IEnumerator Update_WhenHealthIsZero_TriggersDeathLogic()
    {
        health.DealDamage((float)health.maxHealth);

        yield return null;  // Wait for a frame to process the Update method

        Assert.AreEqual("DeadEnemy", testObject.tag);
        Assert.IsFalse(testObject.GetComponent<Collider>().enabled);

        if (health.weaponManager != null)
        {
            Assert.IsFalse(health.weaponManager.blade.enabled);
        }
    }

    [Test]
    public void DealDamage_ReducesHealth()
    {
        var initialHealth = health.CurrentHealth;
        health.DealDamage(1);

        Assert.AreEqual(initialHealth - 1, health.CurrentHealth);
    }

    [Test]
    public void Heal_IncreasesHealth()
    {
        health.DealDamage(1);
        var reducedHealth = health.CurrentHealth;
        health.Heal(1);

        Assert.AreEqual(reducedHealth + 1, health.CurrentHealth);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testObject);
    }
}
