using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTop;
    
    public void Interact()
    {
        var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTop);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }

    public void Select()
    {
        selectedCounterVisual.Show();
    }

    public void DeSelect()
    {
        selectedCounterVisual.Hide();
    }
}
