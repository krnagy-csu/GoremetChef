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
    public TMP_Text weightText;
    
    //Playermovement and playercombat script so I can call the boosts
    public GameObject player;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    
    private void Awake()
    {
        Instance = this;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    public void Add(Item item)
    {
        items.Add(item);
        inventoryWeight += item.weight;
    }

    public void Remove(Item item)
    {
        //Trying buff here?//
        switch (item.itemType)
        {
            case Item.ItemType.StrengthBuff:
                playerCombat.StrengthBoost();
                break;
            case Item.ItemType.SpeedBuff:
                playerMovement.StaminaBoost();
                break;
            case Item.ItemType.StealthBuff:
                playerMovement.StealthBoost();
                break;
        }
        /////////////////////
        
        //After buff is called, remove the item
        items.Remove(item);
        inventoryWeight -= item.weight;
    }
    
    public int GetWeightLimit()
    {
        return inventoryLimit;
    }

    public int GetCurrentWeight()
    {
        return inventoryWeight;
    }

    public void ListItems()
    {
        //Clean list before reopening inventory
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
        // weightText.text = inventoryWeight + " / " + inventoryLimit + " lbs";
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
        EnableRemove.isOn = false;
        // weightText.text = inventoryWeight + " / " + inventoryLimit + " lbs";
    }

    public void CleanList()
    {
        //Clean list before reopening inventory
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        weightText.SetText(GetCurrentWeight() + " / " + GetWeightLimit() + " lbs");
    }
}
