using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    
    
    
    //list of all the ingredients that can be put on the plate.
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSO;
    public List<KitchenObjectSO> KitchenObjectSOList { get; private set; }

    private void Awake()
    {
        KitchenObjectSOList = new List<KitchenObjectSO>();
    }

    
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        
        if (!validKitchenObjectSO.Contains(kitchenObjectSO)) 
        {
            // Not a valid ingredient
            return false;
        }
        if (KitchenObjectSOList.Contains(kitchenObjectSO)) 
        {
            // Already has this type
            return false;
        } 
        else 
        {
            KitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs 
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }
}
