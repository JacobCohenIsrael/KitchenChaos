using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;
    [SerializeField] private Transform counterTop;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        
    }

    public virtual void AltInteract(Player player)
    {
        
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