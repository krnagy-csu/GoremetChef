using System;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public void PickUp()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }
}
