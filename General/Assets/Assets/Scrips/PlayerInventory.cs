using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> inventoryItems;

    public PlayerInventory()
    {
        inventoryItems = new List<string>();
    }

    public void AddItem(string item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveItem(string item)
    {
        inventoryItems.Remove(item);
    }

    public string GetInventoryContents()
    {
        string contents = "Inventory: ";

        foreach (string item in inventoryItems)
        {
            contents += item + ", ";
        }

        return contents.TrimEnd(',', ' ');
    }
}

