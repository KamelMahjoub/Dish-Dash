using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateTaken;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

            OnPlateTaken?.Invoke(this, EventArgs.Empty);
        }
    }
}