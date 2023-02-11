using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace UI
{
    public class TutorialUI : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(object sender, GameManager.State state)
        {
            if (state == GameManager.State.Pending)
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
