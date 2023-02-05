using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;

    private void Start()
    {
        SetProgress(0f);
    }

    public void SetProgress(float progress)
    {
        barImage.fillAmount = progress;

        if ( Math.Abs(progress) < 0.005 || Math.Abs(progress - 1f) < 0.005)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
