using System;
using UnityEngine;

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public static event EventHandler<Transform> OnObjectTrashed;
        public override void Interact(IKitchenObjectParent holder)
        {
            if (holder.HasKitchenObject())
            {
                holder.GetKitchenObject().DestroySelf();
                OnObjectTrashed?.Invoke(this, transform);
            }
        }
    }
}
