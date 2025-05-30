using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {


    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField] private Item kitchenObjectSO;


    public override void Interact(IKitchenObjectParent player) {
        if (!player.HasKitchenObject()) {
            // Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
