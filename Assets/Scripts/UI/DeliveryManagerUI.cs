using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
    }

    private void OnDestroy()
    {
        DeliveryManager.Instance.OnRecipeSpawned -= OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted -= OnRecipeCompleted;
    }

    private void OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (var recipeSo in DeliveryManager.Instance.GetPendingRecipeSoList())
        {
            var recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerRecipeUI>().SetRecipeSo(recipeSo);
        }
    }
}
