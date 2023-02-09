using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesIngredientsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnPlateIngredientAdded;
    }

    private void OnPlateIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (var kitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.GetComponent<PlateIconUI>().SetKitchenObjectSo(kitchenObjectSo);
            iconTransform.gameObject.SetActive(true);
        }
    }
}
