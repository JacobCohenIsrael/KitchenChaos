using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipieSOList;
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
                }
            }
        }
    }

    public override void AltInteract(Player player)
    {
        if (HasKitchenObject() && HasRecipeResult(GetKitchenObject().GetKitchenObjectSO()))
        {
            var outputKitchenObjectSO = GetRecipeResult(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeResult(KitchenObjectSO inputKitchenObjectSO)
    {
        return (from cuttingRecipeSO in cuttingRecipieSOList where cuttingRecipeSO.input == inputKitchenObjectSO select cuttingRecipeSO.output).FirstOrDefault();
    }

    private KitchenObjectSO GetRecipeResult(KitchenObjectSO inputKitchenObjectSO)
    {
        return (from cuttingRecipeSO in cuttingRecipieSOList where cuttingRecipeSO.input == inputKitchenObjectSO select cuttingRecipeSO.output).FirstOrDefault();
    }
}
