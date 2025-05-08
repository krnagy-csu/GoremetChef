using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {


    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    [SerializeField] private Item plateKitchenObjectSO;
    

    public override void Interact(IKitchenObjectParent player) {
        if (!player.HasKitchenObject()) {
            // Player is empty handed
            
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

}