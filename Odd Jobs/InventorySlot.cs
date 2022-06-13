using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour // attached to prefab
{
    [SerializeField] GameObject itemDisplay; // empty game object containing image + text for item
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemStackText;
    
    public Item item;
    public bool slotIsEmpty = true;


    public void AddToSlot(Item item, int stackSize)
    {
        this.item = item;
        slotIsEmpty = false;
        itemImage.sprite = this.item.icon;
        itemStackText.text = stackSize.ToString();
        itemDisplay.SetActive(true);
    }

    public void RemoveFromSlot()
    {
        item = null;
        slotIsEmpty = true;
        itemImage.sprite = null;
        itemDisplay.SetActive(false);
    }

    public void AddToStack(int stackSize)
    {
        itemStackText.text = stackSize.ToString();
    }

    public void RemoveFromStack(int stackSize)
    {
        itemStackText.text = stackSize.ToString();  
    }


}
