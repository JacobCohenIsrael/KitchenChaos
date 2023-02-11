using System;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        [SerializeField] private EventSO trashEvent;
        
        public override void Interact(IKitchenObjectParent holder)
        {
            if (holder.HasKitchenObject())
            {
                holder.GetKitchenObject().DestroySelf();
                trashEvent.Raise(transform);
            }
        }
    }
}
