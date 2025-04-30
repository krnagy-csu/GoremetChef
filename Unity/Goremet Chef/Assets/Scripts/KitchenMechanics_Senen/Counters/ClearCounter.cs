using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Senen Bagos
 * this class handles picking up and putting down ONLY for the clear counter
 * anything can be placed on the clear counter, plates, body parts, ingredients
 */

public class ClearCounter : BaseCounter
{
    [SerializeField] private Item kitchenObjectSO;
    
    public override void Interact(IKitchenObjectParent player) {
        if (!HasKitchenObject()) {
            // The counter is empty
            if (player.HasKitchenObject()) {
                // The player already has something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // The player doesn't have anything
            }
        } else {
            // There is a KitchenObject
            if (player.HasKitchenObject()) {
                // The player is carrying something
                /*if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // The player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // Player is not carrying Plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }*/
            } else {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
    
    // public void Interact(PlayerKitchenInteractions player) {
    //     if (!hasObjectOnCounter()) {
    //         // no object on counter
    //         if (player.hasPlate()) {
    //             // sets plate on counter
    //             setThingOnCounter(player.playerPlate);
    //             player.playerPlate.transform.position = counterTopPoint.transform.position;
    //             player.playerPlate.transform.SetParent(counterTopPoint.transform);
    //             player.clearPlateInHand();
    //             player.changePlateInHand();
    //             return;
    //         }
    //           
    //         if (!player.inventoryIsEmpty()) {
    //             // player has something
    //             
    //             setThingOnCounter(player.getMostRecentItem());
    //             // the players gives most recent item in their inventory to the counter
    //
    //             // Instantiate(getThingOnCounter(), countTopPoint.transform.position, Quaternion.identity);
    //             // ^ this is for debugging if theres something in your inventory already, the game should
    //             // start you off with nothing in your inventory
    //
    //             getThingOnCounter().transform.position = counterTopPoint.transform.position;
    //             getThingOnCounter().SetActive(true);
    //             // puts the object that was given to that counter at the top point and shows it
    //
    //             player.removeFromInventory();
    //             // removes item from invetory
    //         } else {
    //             Debug.Log("Nothing in your inventory");
    //             //player has nothing
    //         }
    //     } else {
    //         Debug.Log("something on counter " + getThingOnCounter());
    //         // will make plating and picking up later
    //         if (getThingOnCounter().CompareTag("Plate") && !player.hasPlate()) {
    //             //checks if thing on counter is a plate and sets it in players hand 
    //             GameObject plate = getThingOnCounter();
    //             if (!player.inventoryIsEmpty()) {
    //                 // if you inventory has something it checks to see if it can be added
    //                 plate.GetComponent<Plate>().TryAddIngridient(player.getMostRecentItem(), player, true);
    //                 if (player.hasPlate()) {
    //                     clearObjectOnCounter();
    //                 }
    //                 return;
    //             }
    //             
    //             plate.transform.SetParent(player.transform);
    //             player.setPlateInHand(plate);
    //             player.changePlateInHand();
    //             clearObjectOnCounter();
    //             return;
    //         }
    //         
    //         if (player.hasPlate()) {
    //             // plate in hand and wants thing on counter to be placed onto the plate
    //             Debug.Log("plate in hand");
    //             GameObject ingredient = getThingOnCounter();
    //             
    //             player.playerPlate.GetComponent<Plate>().TryAddIngridient(ingredient, player, false);
    //             clearObjectOnCounter(); 
    //             return;
    //         }
    //         
    //         if (!player.inventoryHasRoom()) {
    //             Debug.Log("Your inventory is full");
    //             return;
    //         }
    //         
    //         // this section below handles getting things into your inventory from the counter
    //         player.addToInventory(getThingOnCounter());
    //         // adds thing on counter into inventory
    //         getThingOnCounter().SetActive(false);
    //         // hides things on counter
    //         Debug.Log(getThingOnCounter() + " added to inventory");
    //         clearObjectOnCounter();
    //     }
    // }

}

    