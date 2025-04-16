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
                
                // Instantiate(getThingOnCounter(), countTopPoint.transform.position, Quaternion.identity);
                // ^ this is for debugging if theres something in your inventory already, the game should 
                // start you off with nothing in your inventory
                
                getThingOnCounter().transform.position = countTopPoint.transform.position;
                getThingOnCounter().SetActive(true);
                // puts the object that was given to that counter at the top point and shows it
                
                player.removeFromInventory();
                // removes item from invetory 
            } else {
                Debug.Log("Nothing in your inventory");
                //player has nothing 
            }
        } else {
            Debug.Log("something on counter " + getThingOnCounter());
            // will make plating and picking up later
            if (!player.inventoryHasRoom()) {
                Debug.Log("Your inventory is full");
                return;
            } 
            // this section below handles getting things into your inventory from the counter
            
            player.addToInventory(getThingOnCounter());
            // adds thing on counter into inventory
            getThingOnCounter().SetActive(false);
            // hides things on counter
            Debug.Log(getThingOnCounter() + " added to inventory");
            clearObjectOnCounter();
        }
    }
}
