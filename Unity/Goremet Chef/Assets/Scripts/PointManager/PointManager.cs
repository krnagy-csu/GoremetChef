using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointManager : MonoBehaviour
{

    private DeliveryManager deliveryManager;
    public TMP_Text pointText;
    public int points = 0;

    void Start()
    {
        deliveryManager = FindObjectOfType<DeliveryManager>();
    }

    void Update()
    {
        points = deliveryManager.GetSuccessRecipesAmount();
        pointText.text = $"Points: {points}\nGet 5 points to win!";
    }
}
