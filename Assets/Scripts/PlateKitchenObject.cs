using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSo;
    }
    
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSoList;

    private List<KitchenObjectSO> kitchenObjectSoList;

    private void Awake()
    {
        kitchenObjectSoList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        if (!validKitchenObjectSoList.Contains(kitchenObjectSo))
        {
            return false;
            
        }
        if (kitchenObjectSoList.Contains(kitchenObjectSo))
        {
            return false;
        }
        
        kitchenObjectSoList.Add(kitchenObjectSo);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{ KitchenObjectSo = kitchenObjectSo});
        return true;
    }
}
