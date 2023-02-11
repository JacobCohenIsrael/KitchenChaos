using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{

    [SerializeField] private Image image;
    public void SetKitchenObjectSo(KitchenObjectSO kitchenObjectSo)
    {
        image.sprite = kitchenObjectSo.sprite;
    }
}
