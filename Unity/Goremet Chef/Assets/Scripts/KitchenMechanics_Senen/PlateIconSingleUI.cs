using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour {
    
    [SerializeField] private Image image;

    public void SetKitchenObjectSO(Item kitchenObjectSO) {
        image.sprite = kitchenObjectSO.icon;
    }
}