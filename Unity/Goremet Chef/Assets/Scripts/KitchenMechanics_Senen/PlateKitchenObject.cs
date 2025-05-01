using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs {
        public Item kitchenObjectSO;
    }
    
    [SerializeField] private List<Item> validKitchenObjectsSOList;
    
    public List<Item> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<Item>();
    }

    public bool TryAddIngredient(Item kitchenObjectSO) {
        if (!validKitchenObjectsSOList.Contains(kitchenObjectSO)) {
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            
            return true;
        }
    }

    public List<Item> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}