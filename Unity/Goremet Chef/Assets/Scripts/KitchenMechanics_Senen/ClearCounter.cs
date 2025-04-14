using UnityEngine;

public class ClearCounter : BaseCounter {
    // private GameObject thingOnCounter;

    public void Interact(PlayerKitchenInteractions player) {
        if (!hasObjectOnCounter()) {
            // no object on counter
            if (!player.inventoryIsEmpty()) {
                // player has something
                player.getMostRecentItem().gameObject.transform.position = countTopPoint.position;
                player.removeFromInventory();
                //puts the most recent item in inventory on top of the counter
            } else {
                //player has nothing 
            }
        } else {
            Debug.Log("something on counter");
            // will make plating later
        }
    }
}
