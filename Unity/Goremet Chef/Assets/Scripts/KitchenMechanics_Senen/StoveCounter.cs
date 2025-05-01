using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private CookingRecipeSO[] cookingRecipeSOArray;

    private float cookingTimer;
    private CookingRecipeSO activeRecipe;

    public override void Interact(IKitchenObjectParent player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                activeRecipe = GetRecipeForInput(GetKitchenObject().GetKitchenObjectSO());
                cookingTimer = 0f;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                //Move the Item to the player
                GetKitchenObject().SetKitchenObjectParent(player);
                activeRecipe = null;
            }
        }
    }

    private void Update()
    {
        if (HasKitchenObject() && activeRecipe != null)
        {
            cookingTimer += Time.deltaTime;
            if (cookingTimer >= activeRecipe.cookingTimerMax)
            {
                //Clear the Item from the stove (input)
                KitchenObject currentObject = GetKitchenObject();
                currentObject.DestroySelf();  // Make sure DestroySelf also clears the reference

                //Spawn the new item (output)
                KitchenObject.SpawnKitchenObject(activeRecipe.output, this);

                //Get the next recipe ready
                activeRecipe = GetRecipeForInput(activeRecipe.output);
                cookingTimer = 0f;
            }
        }
    }

    private CookingRecipeSO GetRecipeForInput(Item inputItem)
    {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOArray)
        {
            if (cookingRecipeSO.input == inputItem)
            {
                return cookingRecipeSO;
            }
        }
        return null;
    }

    private bool HasRecipe(Item inputItem)
    {
        return GetRecipeForInput(inputItem) != null;
    }
}