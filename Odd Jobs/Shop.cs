using UnityEngine;

public class Shop : Interactable // attached to signboard
{
    [SerializeField] private Item[] shopItems;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private ShopSlot slotPrefab;

    protected override void Awake()
    {
        base.Awake();
        CreateShopDisplay();
    }

    private void CreateShopDisplay()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopSlot temp = Instantiate(slotPrefab);
            temp.transform.SetParent(slotContainer, false);
            temp.item = shopItems[i];
            temp.itemImage.sprite = shopItems[i].icon;
            temp.inventory = FindObjectOfType<Inventory>();
            temp.addButton.onClick.AddListener(delegate { temp.inventory.AddToInventory(temp.item); });
            temp.removeButton.onClick.AddListener(delegate { temp.inventory.RemoveFromInventory(temp.item); });
        }
    }

}
