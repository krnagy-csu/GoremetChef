using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBox : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private List<Item> storedItems = new List<Item>();
    [SerializeField] private Item kitchenObjectSO;

    private void Start()
    {
        storedItems.Clear();
        storedItems.AddRange(InventoryManager.Instance.items);
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (storedItems.Count == 0)
            {
                Debug.Log("Inventory box is empty!");
                return;
            }

            int randomIndex = UnityEngine.Random.Range(0, storedItems.Count);
            Item randomItem = storedItems[randomIndex];
            storedItems.RemoveAt(randomIndex);

            kitchenObjectSO = randomItem;

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

            Debug.Log("Player received: " + randomItem.itemName);
        }
        else
        {
            Debug.Log("Player already has something in their hands.");
        }
    }
}