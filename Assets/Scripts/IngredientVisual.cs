using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class IngredientVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private KitchenObjectSoToGameObject[] kitchenObjectSoToGameObjectMap;
    
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnIngredientAdded;
        foreach (var kitchenObjectSoToGameObject in kitchenObjectSoToGameObjectMap)
        {
            kitchenObjectSoToGameObject.gameObject.SetActive(false);
        }
    }

    private void OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kitchenObjectSoToGameObject in kitchenObjectSoToGameObjectMap)
        {
            if (kitchenObjectSoToGameObject.kitchenObjectSo == e.KitchenObjectSo)
            {
                kitchenObjectSoToGameObject.gameObject.SetActive(true);
            }
        }
    }
}

[Serializable]
public struct KitchenObjectSoToGameObject
{
    public KitchenObjectSO kitchenObjectSo;
    public GameObject gameObject;
}