using UnityEngine;

public class Sell : MonoBehaviour
{
    public GameObject completedFood;
    public GameObject intendedFood;
    private int completionTime;
    private int foodQualityScore;
    private int finalScore;
    public int gradeFood() 
    {
        foodQualityScore = compareFood();
        finalScore =foodQualityScore + rateTime(completionTime);
        return finalScore;
    }
    public int compareFood() 
    {
        //I dont know how but these should compare the food and return a value based on how good it is
        return 0;
    }
    public int rateTime(int completionTime) 
    {
        //I dont know how but this should rate the time and return a value based on how good it is
        return 0;
    }
}
