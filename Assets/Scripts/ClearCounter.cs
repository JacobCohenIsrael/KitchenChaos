using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private SelectedCounterVisual selectedCounterVisual;
    
    public void Interact()
    {
        Debug.Log("Interact");
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
