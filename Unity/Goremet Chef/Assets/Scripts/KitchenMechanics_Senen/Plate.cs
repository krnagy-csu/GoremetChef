using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/* this class manages each unique plate, placing it down and putting and picking it up on clear counters
 * 
 */
public class Plate : MonoBehaviour {
    [SerializeField] private List<GameObject> validIngredients;
    // ^ this list is all of the ingredients that can be on a plate
    [SerializeField] private List<GameObject> ingredientsOnPlate;
    
    public Transform topOfPlate;
    public GameObject plate;
    
    private void Awake() {
        plate = gameObject;
    }

    public void TryAddIngridient(GameObject ingredient, PlayerKitchenInteractions player, bool plateOnCounter) {
        // this checks to see if a certain ingredient can be added to the plate
        if(!ingredient.TryGetComponent(out Ingredient ingredientComponent)) {
            Debug.Log("invalid ingredient");
            if (plateOnCounter) {
                plate.transform.SetParent(player.transform);
                player.setPlateInHand(plate);
                player.changePlateInHand();
                return;
            }
            player.addToInventory(ingredient);
            ingredient.SetActive(false);
            return;
        }
        string ingredientType = ingredientComponent.ingredientName;
        // this gets the name of the ingredient if there is an ingredient 
        
        foreach (var existing in ingredientsOnPlate) {
            // goes through each ingredient on the plate
            if (ingredientsOnPlate.Count == 0) {
                Debug.Log("nothing on plate which is fine");
                return;
            }
            if (existing.GetComponent<Ingredient>().ingredientName == ingredientType) {
                // if an ingredient is already on the plate, places it in players hand
                Debug.Log("Already have this ingredient type on plate");
                plate.transform.SetParent(player.transform);
                player.setPlateInHand(plate);
                player.changePlateInHand();
                return;
            }
        }
        
        // if everything checks out, places ingredient on plate 
        ingredientsOnPlate.Add(ingredient);
        ingredient.transform.position = topOfPlate.transform.position;
        ingredient.transform.SetParent(plate.transform);
        if (plateOnCounter) {
            // this checks if the plate is on the counter and has to remove ingredient from inventory
            ingredient.SetActive(true);
            player.removeFromInventory();    
        }
    }
}
