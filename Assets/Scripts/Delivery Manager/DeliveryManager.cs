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

            if (  GameManager.Instance.IsGamePlaying &&  WaitingRecipeList.Count < waitingRecipeMaxNb)
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
        for (int i = 0; i < WaitingRecipeList.Count; i++)
        {
            PreparedRecipeSO waitingRecipeSO = WaitingRecipeList[i];


            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.KitchenObjectSOList.Count) ;
            {
                bool plateContentsMatchesRecipe = true;
                //has the same number of ingredients

                foreach (var recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    //cycling through all ingredients in the recipe
                    foreach (var plateKitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
                    {
                        //cycling through all ingredient on the plate

                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
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
                    Debug.Log("Player Delivered the correct recipe");
                    WaitingRecipeList.RemoveAt(i);

                    SuccessfulRecipes++;

                    OnRecipeCompleted.Invoke(this, EventArgs.Empty);

                    OnRecipeSuccess.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        //No matches found!
        Debug.Log("The player did not deliver the correct recipe!");
        OnRecipeFailed.Invoke(this, EventArgs.Empty);
    }
}