using System;
using UnityEngine;
using UnityEngine.Serialization;


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
    public Transform dropPosition;
    public Transform platePosition;
    
    public GameObject playerPlate;
    public bool plateInHand = false;
    
    public event Action OnInventoryChanged;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            PickUpPlaceDown();
        }
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            DropItem();
        }
    }

    // if collides with an object that is PickUp-able will add it to inventory 
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            if (!inventoryHasRoom()) {
                Debug.Log("Inventory full!");
                return;
            }
            
            GameObject pickUp = other.gameObject;
            addToInventory(pickUp);
            pickUp.SetActive(false);
            //Hides the pick up item in the scene
            Debug.Log(pickUp + " added to inventory");
        }
        
    }

    private void DropItem() {
        // sends a raycast to see if it hits nothing
        if (!Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, 2f)) {
            if (inventoryIsEmpty()) {
                Debug.Log("Inventory is empty");
                return;
                // if nothing is in your inventory does nothing
            }
            
            getMostRecentItem().transform.position = dropPosition.position;
            getMostRecentItem().SetActive(true);
            removeFromInventory();
            // this drops your most recent item at your feet then removes it from your inventory
        }
        Debug.Log("Something is in the way");
    }
    
    private void PickUpPlaceDown() {
        // sends a forward raycast to see which object it hits
        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out RaycastHit hit, 2f)) {
            
            if (hit.collider.gameObject.CompareTag("ClearCounter")) {
                ClearCounter clearCounter = hit.collider.gameObject.GetComponent<ClearCounter>();
                //clearCounter.Interact(this);
                // Debug.Log("Got ClearCounter component!");
            }
            
            if (hit.collider.gameObject.CompareTag("PlateCounter")) {
                PlateCounter plateCounter = hit.collider.gameObject.GetComponent<PlateCounter>();
                plateCounter.Interact(this);
                Debug.Log("Got plateCounter component!");
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
            OnInventoryChanged?.Invoke();
        }
    }
    
    // should be called in pair with getMostRecentItem
    public void removeFromInventory() {
        if (inventoryIsEmpty()) {
            Debug.Log("Nothing in your inventory");
        } else {
            inventory[top] = null;
            top--;
            OnInventoryChanged?.Invoke();
        }
    }

    public bool inventoryHasRoom() {
        return top != 4;
    }
    
    public bool inventoryIsEmpty() {
        return top == -1;
    }
    
    public GameObject[] getInventoryArray() {
        return inventory;
    }

    public void setPlateInHand(GameObject plate) {
        playerPlate = plate;
    }
    
    public void changePlateInHand() {
        plateInHand = !plateInHand;
    }
    
    public bool hasPlate() {
        return plateInHand;
    }

    public void clearPlateInHand() {
        playerPlate = null;
    }
}
