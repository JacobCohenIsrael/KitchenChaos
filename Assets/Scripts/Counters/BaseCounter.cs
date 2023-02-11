using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static EventHandler<Transform> OnKitchenObjectPlaced;
    
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;
    [SerializeField] private Transform counterTop;

    private KitchenObject kitchenObject;
    public virtual void Interact(IKitchenObjectParent holder)
    {
        // CR: No need to interact with player
    }

    public virtual void AltInteract(IKitchenObjectParent holder)
    {
        // CR: No need to interact with player, are we even using the holder?
    }
    
    public void Select()
    {
        selectedCounterVisual.Show();
    }

    public void DeSelect()
    {
        selectedCounterVisual.Hide();
    }
    
        
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTop;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnKitchenObjectPlaced?.Invoke(this, transform);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
