using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipieSOList;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;

    private int cuttingProgress;
    
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeResult(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    var cuttingRecipeSO = GetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());

                    var progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;
                    progressBarUI.SetProgress(progressNormalized);
                }
            }
        }
    }

    public override void AltInteract(Player player)
    {
        if (HasKitchenObject() && HasRecipeResult(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            cuttingCounterVisual.PlayCut();
            var cuttingRecipeSO = GetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());
            var progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;
            progressBarUI.SetProgress(progressNormalized);
            if (cuttingProgress < cuttingRecipeSO.cuttingProgressMax) return;
            
            var outputKitchenObjectSO = GetRecipeResult(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeResult(KitchenObjectSO inputKitchenObjectSO)
    {
        var cuttingRecipe = GetCuttingRecipe(inputKitchenObjectSO);
        return cuttingRecipe != null;
    }

    private KitchenObjectSO GetRecipeResult(KitchenObjectSO inputKitchenObjectSO)
    {
        var cuttingRecipe = GetCuttingRecipe(inputKitchenObjectSO);
        return cuttingRecipe != null ? cuttingRecipe.output : null;
    }

    private CuttingRecipeSO GetCuttingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return (from cuttingRecipeSO in cuttingRecipieSOList where cuttingRecipeSO.input == inputKitchenObjectSO select cuttingRecipeSO).FirstOrDefault();

    }
}
