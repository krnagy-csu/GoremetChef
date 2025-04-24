using System;
using UnityEngine;

public class PlateCounter : BaseCounter {
    
    public GameObject platePrefab;

    public void Interact(PlayerKitchenInteractions player) {
        if (!player.hasPlate()) {
            GameObject plate = Instantiate(platePrefab, player.platePosition.transform.position, Quaternion.identity);
            plate.transform.SetParent(player.transform);
            player.setPlateInHand(plate);
            player.changePlateInHand();
        }
    }
}
