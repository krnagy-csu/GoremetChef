using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {
    [SerializeField] private List<IngredientSO> validIngredients;
    private List<IngredientSO> ingredientsOnPlate;
    
    public Transform topOfPlate;
    public GameObject plate;
    private bool plateInHand = false;
    
    public void TryAddIngridient(IngredientSO ingredient) {
        if (!validIngredients.Contains(ingredient)) {
            return;
        }
        if (ingredientsOnPlate.Contains(ingredient)) {
            return;
        }
        ingredientsOnPlate.Add(ingredient);
        

    }

    public void setPlateInHand() {
        plateInHand = true;
    }

    public void removePlateInHand() {
        plateInHand = false;
    }
    
    public bool isPlateInHand() {
        return true;
    }
}
