using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoupRecipeSO : ScriptableObject
{
    public List<Item> ingredientList;
    public Item output;
    public float cookingTimerMax;
}