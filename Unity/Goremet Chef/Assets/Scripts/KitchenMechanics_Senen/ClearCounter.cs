using UnityEngine;

/* Senen Bagos
 * this class handles picking up and putting down ONLY for the clear counter
 * anything can be placed on the clear counter, plates, body parts, ingredients
 */
public class ClearCounter : BaseCounter {

    public void Interact(PlayerKitchenInteractions player) {
        if (!hasObjectOnCounter()) {
            // no object on counter
            if (!player.inventoryIsEmpty()) {
                // player has something
                
                setThingOnCounter(player.getMostRecentItem());
                // the players gives most recent item in their inventory to the counter 
                Instantiate(getThingOnCounter(), countTopPoint.transform.position, Quaternion.identity);
                // spawns in the object that was given to that counter at the top point
                player.removeFromInventory();
                // removes item from invetory 
            } else {
                Debug.Log("Nothing in your inventory");
                //player has nothing 
            }
        } else {
            Debug.Log("something on counter " + getThingOnCounter());
            // will make plating and picking up later
        }
    }
}
