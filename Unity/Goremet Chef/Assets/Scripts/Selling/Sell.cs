using UnityEngine;

public class Sell : MonoBehaviour
{
    public Recipe targetRecipe;
    public GameObject finishedFoodObject;

    public void Evaluate(Recipe tr, GameObject food) 
    {
        targetRecipe = tr;
        finishedFoodObject = food;
        Evaluate();
    }

    public void Evaluate()
    {
        if (finishedFoodObject == null || targetRecipe == null) return;

        FinishedFood finished = finishedFoodObject.GetComponent<FinishedFood>();
        if (finished == null)
        {
            Debug.LogError("FinishedFood component not found!");
            return;
        }

        float score = CalculateMatchScore(targetRecipe, finished);
        Debug.Log("Recipe Match Score: " + score);
    }

    private float CalculateMatchScore(Recipe recipe, FinishedFood finished)
    {
        float total = 0f;
        float matched = 0f;

        // Sum total expected ingredients
        foreach (var ingredient in recipe.ingredients)
        {
            total += ingredient.amount;

            var match = finished.finalIngredients.Find(f => f.name == ingredient.name);
            if (match != null)
            {
                float ratio = Mathf.Min(match.amount, ingredient.amount) / (float)ingredient.amount;
                //here ratio determines how much of the right stuff they have
                matched += ingredient.amount * ratio;
                //ensures partial credit
            }
        }

        return (total > 0f) ? matched / total : 0f;
    }
}

