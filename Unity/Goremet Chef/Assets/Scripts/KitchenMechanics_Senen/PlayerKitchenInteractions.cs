using System;
using UnityEngine;


/* Senen Bagos
 * this class will handle the interaction of picking up and placing down objects on the counters
 * and cookware. 
 * 
 */
public class PlayerKitchenInteractions : MonoBehaviour {
    
    [SerializeField] private GameObject[] inventory = new GameObject[5]; 
    //inventory size of 5, can be changed in future
    [SerializeField] private int top = -1; 
    // stack structure IN INSPECTOR YOU CAN EDIT IT AND THATS STRICTLY FOR TESTING
    public Transform raycastOrigin;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            PlaceDown();
        }
    }

    private void PlaceDown() {
        // sends a forward raycast to see which object it hits
        // it can hit in object but only checks for counters
        if (Physics.Raycast(raycastOrigin.position, Vector3.forward * 2, out RaycastHit hit)) {
            if (hit.collider.gameObject.CompareTag("ClearCounter")) {
                ClearCounter clearCounter = hit.collider.gameObject.GetComponent<ClearCounter>();
                if (clearCounter is not null) {
                    // if a clear counter exists, call this function that will handle picking up
                    // and putting down
                    clearCounter.Interact(this);
                    Debug.Log("Got ClearCounter component!");
                }
            }
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
