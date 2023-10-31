using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
  
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object on the counter
            if (player.HasKitchenObject())
            {
                //player is carrying something
                //drop it on the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player has nothing in hand
            }
        }
        else
        {
            //there is a kitchen object on the counter
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
         
                    //gets the kitchen object thats on the counter
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //the player is not carrying a plate bt something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //there s a plate on the counter
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().KitchenObjectSO))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }

                    }
                }
     
            }
            else
            {
                //player isnt carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        
        }
    }

}
