using System;
using UnityEngine;


/* Senen Bagos
 * 
 * 
 */
public class PlayerKitchenInteractions : MonoBehaviour {
    
    [SerializeField] private GameObject[] inventory = new GameObject[5]; //inventory size of 5
    private int top = -1; // stack structure
    
    private void Update() {

        if (Input.GetKeyDown(KeyCode.E)) {
            
        }
    }
    
    // should be called in pair with removeFromInventory
    public GameObject getMostRecentItem() {
        if(inventoryIsEmpty()){
            Debug.Log("Empty Inventory");
            return null;
        } 
        return inventory[top];
    }
    
    //adds item to inventory
    public void addToInventory(GameObject item) {
        if (top == inventory.Length - 1) {
            Debug.Log("Inventory is full");
        } else {
            inventory[++top] = item;
        }
    }
    
    // should be called in pair with getMostRecentItem
    public void removeFromInventory() {
        if (inventoryIsEmpty()) {
            Debug.Log("Nothing in your inventory");
        } else {
            top--;
        }
    }
    public bool inventoryIsEmpty() {
        return top == -1;
    }
}
