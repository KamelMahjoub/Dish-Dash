using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleIngredientIcon : MonoBehaviour
{
    [SerializeField] private Image image;
    
    public void SetKitchenObjectSo(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
