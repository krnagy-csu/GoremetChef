using System;
using UnityEngine;


/* Senen Bagos
 * 
 * 
 */
public class PlayerKitchenInteractions : MonoBehaviour {
    
    [SerializeField] private GameObject[] inventory = new GameObject[5]; //inventory size of 5
    [SerializeField] private int top = -1; // stack structure
    public Transform raycastOrigin;
    
    private void Update() {

        if (Input.GetKeyDown(KeyCode.E)) {
            PlaceDown();
        }
    }

    private void PlaceDown() {
        if (Physics.Raycast(raycastOrigin.position, Vector3.forward * 2, out RaycastHit hit)) {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
    
    void OnDrawGizmos()
    {
        if (raycastOrigin == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(raycastOrigin.transform.position, raycastOrigin.transform.forward*2);
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
