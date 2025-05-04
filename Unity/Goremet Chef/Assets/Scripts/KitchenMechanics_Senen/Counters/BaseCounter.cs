using System;
using UnityEngine;


/* Senen Bagos
 * this class is the parent of all counters that holds the basic methods
 * controlling the basic interaction of placing or pickup on the counter
 *
 */
public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    
    public static event EventHandler OnAnyObjectPlacedHere;

    public Transform counterTopPoint;
    public GameObject thingOnCounter;
    
    private KitchenObject kitchenObject;
    
    public virtual void Interact(IKitchenObjectParent player) {
        Debug.LogError("BaseCounter.Interact();");
    }
    
    public virtual void AlternateInteract(IKitchenObjectParent player) {
        Debug.LogError("BaseCounter.AlternateInteract();");
    }
    
    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
    
    // gives the specific counter the object that is passed in and owns it
    public void setThingOnCounter(GameObject ingredient) {
        if (ingredient == null) {
            Debug.Log("Nothing in your inventory");
            return;
        }

        if (hasObjectOnCounter()) {
            Debug.Log("Something is already on the counter "+ thingOnCounter.name);
            return;
        }
        
        thingOnCounter = ingredient;
    }
    
    public void clearObjectOnCounter() {
        thingOnCounter = null;
    }

    public GameObject getThingOnCounter() {
        return thingOnCounter;
    }
    
    public bool hasObjectOnCounter() {
        return thingOnCounter != null;
    }

}
