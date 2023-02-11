using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visuals;

    public void Show()
    {
        foreach (var visual in visuals)
        {
            visual.SetActive(true);

        }
    }

    public void Hide()
    {
        foreach (var visual in visuals)
        {
            visual.SetActive(false);
        }
    }
}
