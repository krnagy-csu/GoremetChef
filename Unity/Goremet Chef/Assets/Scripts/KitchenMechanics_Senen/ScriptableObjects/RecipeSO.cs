using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject{
    public string recipeName;
    public List<GameObject> ingredientList;
}
