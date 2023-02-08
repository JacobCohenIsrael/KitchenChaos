using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(IKitchenObjectParent holder)
    {
        if (holder.HasKitchenObject())
        {
            if (holder.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
            {
                holder.GetKitchenObject().DestroySelf();
            }
        }
    }
}
