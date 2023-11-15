using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private PreparedRecipeListSO recipeListSO;
    public List<PreparedRecipeSO> WaitingRecipeList { get; private set; }

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMaxNb = 4;
    public int SuccessfulRecipes { get; private set; }


    private void Awake()
    {
        Instance = this;

        WaitingRecipeList = new List<PreparedRecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying && WaitingRecipeList.Count < waitingRecipeMaxNb)
            {
                PreparedRecipeSO waitingRecipe =
                    recipeListSO.preparedRecipeList[Random.Range(0, recipeListSO.preparedRecipeList.Count)];

                WaitingRecipeList.Add(waitingRecipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (var waitingRecipe in WaitingRecipeList)
        {

            if (waitingRecipe.kitchenObjectSOList.Count == plateKitchenObject.KitchenObjectSOList.Count)
            {
                bool plateContentsMatchesRecipe = true;

                foreach (var recipeKitchenObjectSO in waitingRecipe.kitchenObjectSOList)
                {
                    bool ingredientFound = false;


                    //cycling through all ingredients in the recipe
                    foreach (var plateKitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
                    {
                        //cycling through all ingredient on the plate

                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            Debug.Log(plateKitchenObjectSO);
                            //Ingredients match
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //This recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //player delivered the correct recipe

                    WaitingRecipeList.Remove(waitingRecipe);

                    SuccessfulRecipes++;

                    OnRecipeCompleted.Invoke(this, EventArgs.Empty);

                    OnRecipeSuccess.Invoke(this, EventArgs.Empty);
                    return;
                }



            }
        }

        OnRecipeFailed.Invoke(this, EventArgs.Empty);  
        
    }
}