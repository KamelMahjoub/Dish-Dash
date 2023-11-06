using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientIcons : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
    
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
        {
            
            Transform iconGameObject =  Instantiate(iconTemplate, transform);
            iconGameObject.gameObject.SetActive(true);
            iconGameObject.GetComponent<SingleIngredientIcon>().SetKitchenObjectSo(kitchenObjectSO);
        }
    }
}
