using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }
    
    public RecipeSO targetRecipe;
    private void Awake() {
            Instance = this;
    }

    public void setCurrentRecipe(RecipeSO recipe) {
        targetRecipe = recipe;
    }
    
    /*public override void Interact(Character player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

                player.GetKitchenObject().DestroySelf();
            }
        }
    }*/
}