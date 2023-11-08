using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }


    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;

    [SerializeField] private GameObject counterKnife;
    [SerializeField] private GameObject counterKnifeSelected;

    [SerializeField] private GameObject progressBar;

    private int cuttingProgress;


    private void Start()
    {
        progressBar.SetActive(false);
    }


    public new static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object on the counter
            if (player.HasKitchenObject())
            {
                //player is carrying something
                //If the player is carrying an ingredient that can be sliced/cooked
                if (HasRecipeWithInput(player.GetKitchenObject().KitchenObjectSO))
                {
                    //drop it on the counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);

                    cuttingProgress = 0;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    progressBar.SetActive(false);

                    //gets the kitchen object thats on the counter
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //player isnt carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                progressBar.SetActive(false);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().KitchenObjectSO))
        {
            //there is a kitchen object here and it can be cut

            progressBar.SetActive(true);

            PlayCutAnimation();

            counterKnife.SetActive(false);
            counterKnifeSelected.SetActive(false);


            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);


            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            StartCoroutine(Cooldown());
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });


            

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().KitchenObjectSO);

                //can cut
                GetKitchenObject().DestroySelf();

                progressBar.SetActive(false);

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.95f);
        counterKnife.SetActive(true);
        counterKnifeSelected.SetActive(true);
    }
    
 


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO != null)
            return cuttingRecipeSO.output;
        else
            return null;
    }


    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSoArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }

    private void PlayCutAnimation()
    {
        Player.Instance.Cut();
    }
}