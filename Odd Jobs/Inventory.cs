using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameManager GM;

    [SerializeField] private InventorySlot[] inventorySlots; // to visually indicate what is in the inventory
    public Dictionary<Item, int> inventoryDict = new Dictionary<Item, int>(); // to keep track of what is in the inventory


    private void OnEnable()
    {
        Home.ResetEvent += ResetInventory;
    }

    private void OnDisable()
    {
        Home.ResetEvent -= ResetInventory;
    }



    public void AddToInventory(Item item)
    {
        if (inventoryDict.TryGetValue(item, out int stackSize)) // if item is already in inventory
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].item == item)
                {
                    stackSize++;
                    inventoryDict[item] = stackSize;
                    inventorySlots[i].AddToStack(stackSize);
                    return;
                }
            }   
        }

        else
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].slotIsEmpty)
                {
                    inventoryDict.Add(item, 1);
                    inventorySlots[i].AddToSlot(item, 1);
                    return;
                }
            }
        }  
    }


    public void RemoveFromInventory(Item item)
    {
        if (inventoryDict.TryGetValue(item, out int stackSize))
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].item == item)
                {
                    if (stackSize == 1)
                    {
                        inventoryDict.Remove(item);
                        inventorySlots[i].RemoveFromSlot();
                        return;
                    }

                    else if (stackSize > 1)
                    {
                        stackSize--;
                        inventoryDict[item] = stackSize;
                        inventorySlots[i].RemoveFromStack(stackSize);
                        return;
                    }
                }
            }
        }
    }


    public void ResetInventory()
    {
        inventoryDict.Clear();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].RemoveFromSlot();
        }
    }


}
