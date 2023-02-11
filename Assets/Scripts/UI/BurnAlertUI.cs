using System;
using Counters;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class BurnAlertUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private float burnAlertThreshold = 0.5f;

        private void Start()
        {
            Hide();
            stoveCounter.OnProgressChanged += OnProgressChanged;
        }

        private void OnProgressChanged(object sender, float progress)
        {
            var show = stoveCounter.IsFried() && progress >= burnAlertThreshold;

            if (show)
            {
                Show();
            }
            else
            {
                Hide();
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
}
