using System;
using TMPro;
using UnityEngine;

public class KitchenInventoryUI : MonoBehaviour {
    public PlayerKitchenInteractions player;
    public TextMeshProUGUI inventoryText;
    
    private void Start() {
        player.OnInventoryChanged += UpdateInventoryDisplay;
        UpdateInventoryDisplay();
    }
    
    private void OnDestroy() {
        player.OnInventoryChanged -= UpdateInventoryDisplay; 
    }

    // public void UpdateInventoryDisplay() {
    //     if (player.inventoryIsEmpty()) {
    //         inventoryText.text = "Inventory: Empty";
    //     } else {
    //         GameObject recentItem = player.getMostRecentItem();
    //         inventoryText.text = "Recent Item: " + recentItem.name;
    //     }
    // }
    
    private void UpdateInventoryDisplay() {
        GameObject[] items = player.getInventoryArray();
        int top = GetTopIndex(items);

        string display = "Inventory:\n";
        for (int i = top; i >= 0; i--) {
            display += items[i] != null ? items[i].name + "\n" : "Empty\n";
        }

        for (int i = top + 1; i < items.Length; i++) {
            display += "Empty\n";
        }

        inventoryText.text = display;
    }

    private int GetTopIndex(GameObject[] items) {
        for (int i = items.Length - 1; i >= 0; i--) {
            if (items[i] != null) return i;
        }
        return -1;
    }
}
