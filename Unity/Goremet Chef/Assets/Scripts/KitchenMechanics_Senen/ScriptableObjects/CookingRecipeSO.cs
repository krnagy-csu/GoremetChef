using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CookingRecipeSO : ScriptableObject {
    public Item input;
    public Item output;
    public float cookingTimerMax;
}
