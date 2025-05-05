using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoupCounter : BaseCounter {

    private List<Item> currentIngredients = new List<Item>();
    private float cookTimer;
    private bool isCooking;

    [SerializeField] private List<SoupRecipeSO> soupRecipes;

    private SoupRecipeSO currentRecipe;

    public override void Interact(IKitchenObjectParent player) {
        if (player.HasKitchenObject()) {
            KitchenObject kitchenObject = player.GetKitchenObject();
            Item ingredient = kitchenObject.GetKitchenObjectSO();

            if (!isCooking && CanAcceptIngredient(ingredient)) {
                currentIngredients.Add(ingredient);
                kitchenObject.DestroySelf();
                TryStartCooking();
            }

        } else if (isCooking && cookTimer <= 0f && !HasKitchenObject()) {
            KitchenObject.SpawnKitchenObject(currentRecipe.output, this);
            ResetCounter();

        } else if (!player.HasKitchenObject() && HasKitchenObject()) {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    private void TryStartCooking() {
        foreach (SoupRecipeSO recipe in soupRecipes) {
            if (HasAllIngredients(recipe)) {
                currentRecipe = recipe;
                isCooking = true;
                cookTimer = currentRecipe.cookingTimerMax;
                break;
            }
        }
    }

    private bool HasAllIngredients(SoupRecipeSO recipe) {
        if (currentIngredients.Count != recipe.ingredientList.Count) return false;

        var required = new List<Item>(recipe.ingredientList);
        foreach (var ingredient in currentIngredients) {
            if (required.Contains(ingredient)) {
                required.Remove(ingredient);
            } else {
                return false;
            }
        }

        return required.Count == 0;
    }


    private bool CanAcceptIngredient(Item ingredient) {
        return currentIngredients.Count < 3;
    }

    private void Update() {
        if (isCooking) {
            cookTimer -= Time.deltaTime;
            if (cookTimer <= 0f) {
                cookTimer = 0f;
                KitchenObject.SpawnKitchenObject(currentRecipe.output, this);
                ResetCounter();
            }
        }
    }

    private void ResetCounter() {
        currentIngredients.Clear();
        currentRecipe = null;
        isCooking = false;
    }
}