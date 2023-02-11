using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
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
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(holder.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            holder.GetKitchenObject().DestroySelf();
                        }
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
                holder.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    }
}
