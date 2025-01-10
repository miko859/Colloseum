using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ItemUITests
{
    private GameObject itemUIObject;
    private GameObject itemUI;
    private InventoryItemController inventoryItemController;
    private Button itemButton;

    private GameObject player;
    private Health mockHealth;

    [SetUp]
    public void SetUp()
    {
        // Vytvorenie hráča
        player = new GameObject("Player");
        mockHealth = player.AddComponent<Health>();
        mockHealth.maxHealth = 100;

        // Vytvorenie InventoryItemController
        var inventoryObject = new GameObject("InventoryItemController");
        inventoryItemController = inventoryObject.AddComponent<InventoryItemController>();

        // Vytvorenie ItemUI
        itemUIObject = new GameObject("ItemUI");
           itemButton = itemUIObject.AddComponent<Button>();
       var item = itemUI.AddComponent<InventoryItemController>();
        // Pridanie komponentov do ItemUI
        //itemUI.itemButton = itemButton;
        //itemUI.inventoryItemController = inventoryItemController;

        // Nastavenie tlačidla
        //itemUI.Awake();
    }

    [Test]
    public void ItemUI_OnClick_UsesHealthPotion()
    {
        // Arrange: Pridáme Health Potion
        var healingPotion = ScriptableObject.CreateInstance<PotionData>();
        healingPotion.potionType = PotionType.HEALTH;
        healingPotion.amount = 25;
        healingPotion.currentStack = 1;

        mockHealth.SetCurrentHealth(50);
        inventoryItemController.AddItem(healingPotion);

        // Act: Simulujeme kliknutie na tlačidlo
        itemButton.onClick.Invoke();

        // Assert: Skontrolujeme, či sa použil potion
        Assert.AreEqual(75, mockHealth.GetCurrentHealth(), "Player's health should increase by 25.");
        Assert.AreEqual(0, healingPotion.currentStack, "Potion stack should reduce to 0.");
    }

    [TearDown]
    public void TearDown()
    {
        // Vyčistenie objektov
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(itemUIObject);
        Object.DestroyImmediate(inventoryItemController.gameObject);
    }
}
