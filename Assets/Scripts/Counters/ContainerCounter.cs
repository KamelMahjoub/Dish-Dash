using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerPickedUpObject; 

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
    
    
    public override void Interact(Player player)
    {
        //if the player has no object in hand
        if (!player.HasKitchenObject() && GameManager.Instance.IsGamePlaying)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            
            OnPlayerPickedUpObject?.Invoke(this,EventArgs.Empty);
        }
     
    }
}
