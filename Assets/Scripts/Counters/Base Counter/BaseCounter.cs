using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectHolder
{
   
    public static event EventHandler onAnyObjectPlacedHere;
    
    [SerializeField] private Transform counterTopPoint;
    

    //the kitchen object on top of the counter
    private KitchenObject kitchenObject;
    
    
    public static void ResetStaticData()
    {
        onAnyObjectPlacedHere = null;
    }
    
    

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }
    
    public virtual void InteractAlternate(Player player)
    { 
        Debug.LogError("BaseCounter.InteractAlternate()");
    }

    public KitchenObject GetKitchenObject() => kitchenObject;
  
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            onAnyObjectPlacedHere?.Invoke(this,EventArgs.Empty);
        }
    }
  
    public bool HasKitchenObject() => kitchenObject != null;
  
    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
  
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
