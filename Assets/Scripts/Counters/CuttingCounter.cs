using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter
{
    // CR: make the event handler send the transform instead of using class implicit on the sender
    public static event EventHandler<Transform> OnAnyCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipieSOList;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;

    private int cuttingProgress;
    
    public override void Interact(IKitchenObjectParent holder)
    {
        if (HasKitchenObject())
        {
            if (holder.HasKitchenObject())
            {
                if (holder.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(holder);
            }
        }
        else
        {
            if (holder.HasKitchenObject())
            {
                if (HasRecipeResult(holder.GetKitchenObject().GetKitchenObjectSO()))
                {
                    holder.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    var cuttingRecipeSO = GetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());

                    var progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;
                    progressBarUI.SetProgress(progressNormalized);
                }
            }
        }
    }

    public override void AltInteract(IKitchenObjectParent holder)
    {
        if (HasKitchenObject() && HasRecipeResult(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            cuttingCounterVisual.PlayCut();
            OnAnyCut?.Invoke(this, transform);
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
