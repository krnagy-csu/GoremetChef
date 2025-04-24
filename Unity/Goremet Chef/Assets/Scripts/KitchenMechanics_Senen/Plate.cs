using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Plate : MonoBehaviour {
    [SerializeField] private List<GameObject> validIngredients;
    [SerializeField] private List<GameObject> ingredientsOnPlate;
    
    public Transform topOfPlate;
    public GameObject plate;

    private void Awake() {
        plate = this.gameObject;
    }

    public void TryAddIngridient(GameObject ingredient, PlayerKitchenInteractions player) {
        
        if(!ingredient.TryGetComponent(out Ingredient ingredientComponent)) {
            return;
        }
        string ingredientType = ingredientComponent.ingredientName;

        // foreach (var existing in validIngredients) {
        //     if (existing.GetComponent<Ingredient>().ingredientName == ingredientType) {
        //         Debug.Log("Valid ingredient found");
        //     }
        // }
        
        foreach (var existing in ingredientsOnPlate) {
            if (ingredientsOnPlate.Count == 0) {
                Debug.Log("nothing on plate which is fine");
                return;
            }
            if (existing.GetComponent<Ingredient>().ingredientName == ingredientType) {
                Debug.Log("Already have this ingredient type on plate");
                return;
            }
        }
        
        
        ingredientsOnPlate.Add(ingredient);
        ingredient.transform.position = topOfPlate.transform.position;
        ingredient.transform.SetParent(plate.transform);
        ingredient.SetActive(true);
        player.removeFromInventory();
    }
}
