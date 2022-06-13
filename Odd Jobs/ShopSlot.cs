using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour // attached to prefab
{
    [HideInInspector] public Inventory inventory;
    [HideInInspector] public Item item; // scriptable object

    public Button addButton; // attached to button
    public Button removeButton; // attached to button
    public Image itemImage;

}
