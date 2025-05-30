using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

//Scriptable object for all the item pick-ups.
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int weight;
    public Sprite icon;
    public Transform prefab;
    public ItemType itemType;

    public enum ItemType
    {
        StrengthBuff,
        SpeedBuff,
        StealthBuff
    }
}
