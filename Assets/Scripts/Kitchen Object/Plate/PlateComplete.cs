using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateComplete : MonoBehaviour
{
    [Serializable] //in order for it to show up in the inspector.
    public struct KichenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    
    
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KichenObjectSO_GameObject> kichenObjectSO_GameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kitchenObjectSo_GameObject in kichenObjectSO_GameObjectList)
        {
            if (kitchenObjectSo_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSo_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
