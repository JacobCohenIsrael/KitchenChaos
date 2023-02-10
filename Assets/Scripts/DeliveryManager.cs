using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;
    
    // CR: This is bad, because it creates a tight coupling and testing becomes hard
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSo;
    [SerializeField] private float spawnRecipeTimer;
    [SerializeField] private float spawnRecipeTimerMax = 4;
    [SerializeField] private float maxPendingRecipes = 4;
    private List<RecipeSO> pendingRecipeSoList;

    private void Awake()
    {
        Instance = this;
        pendingRecipeSoList = new List<RecipeSO>();
        // CR: perhaps create a Dictionary<> so we don't have to loop over n square
    }

    private void Update()
    {
        // CR: no need to inc timers
        if (pendingRecipeSoList.Count >= maxPendingRecipes) return;
        
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            var pendingRecipeSo = recipeListSo.recipeSoList[Random.Range(0, recipeListSo.recipeSoList.Count)];
            pendingRecipeSoList.Add(pendingRecipeSo);
            
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        // CR: should we be able deliver an empty plate?
        for (int i = 0; i < pendingRecipeSoList.Count; i++)
        {
            var pendingRecipeSo = pendingRecipeSoList[i];

            if (pendingRecipeSo.kitchenObjectSoList.Count == plateKitchenObject.GetKitchenObjectSoList().Count)
            {
                var plateContentsMatchesRecipe = true;
                foreach (var recipeKitchenObjectSo in pendingRecipeSo.kitchenObjectSoList)
                {
                    var ingredientFound = false;
                    foreach (var plateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        if (plateKitchenObjectSo == recipeKitchenObjectSo)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    pendingRecipeSoList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    
                    return;
                }
            }
        }
        
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetPendingRecipeSoList()
    {
        return pendingRecipeSoList;
    }
}
