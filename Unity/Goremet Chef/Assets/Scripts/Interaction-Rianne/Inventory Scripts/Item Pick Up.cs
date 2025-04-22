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

    //I don't know how we feel about using mouse down I would prefer for it to be when you hit E and the item is within range of the player but I just couldn't figure out how to make it happen
    private void OnMouseDown()
    {
        PickUp();
    }
}
