using UnityEngine;
using UnityEngine.UI;
public class InventoryItemController : MonoBehaviour
{ 
    Item item;
    
    public Button RemoveButton;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;

    public void RemoveItem()
    {
        UseItem();
        InventoryManager.Instance.Remove(item);
        
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    //This is very much not working but I don't wanna risk losing my progress so let's end here
    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.SpeedBuff:
                playerMovement.SpeedBoost(5);
                break;
            case Item.ItemType.StrengthBuff:
                playerCombat.StrengthBoost(5);
                break;
            case Item.ItemType.StealthBuff:
                playerMovement.StealthBoost(5);
                break;
        }
    }
}
