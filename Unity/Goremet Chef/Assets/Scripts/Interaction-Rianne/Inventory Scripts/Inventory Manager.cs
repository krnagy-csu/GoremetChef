using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;
    
    public InventoryItemController [] InventoryItems;

    //How much the inventory weighs and the limit you can carry, public so they can be checked from player? Who knows.
    public int inventoryWeight;
    public int inventoryLimit = 15; //This can be changed, just temp
    
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        //Clean list before reopening inventory
        // foreach (Transform item in ItemContent)
        // {
        //     Destroy(item.gameObject);
        // }
        CleanList();
        
        foreach (var item in items)
        {
            //Getting the name and icon components to update depending on the item
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            
            var removeButton = obj.transform.Find("Remove Button").GetComponent<Button>();
            
            //Displays the item's name and weight in the inventory
            itemName.text = item.itemName + " (" + item.weight + "lbs)";
            itemIcon.sprite = item.icon;

            //If the remove toggle is active, you'll see the X button beside each icon.
            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("Remove Button").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("Remove Button").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            InventoryItems[i].AddItem(items[i]);
        }
    }

    public void CleanList()
    {
        //Clean list before reopening inventory
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
}
