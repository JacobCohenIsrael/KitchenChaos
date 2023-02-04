using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTop;

    private KitchenObject kitchenObject;
    
    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTop);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            kitchenObjectTransform.localPosition = Vector3.zero;

            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }

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
