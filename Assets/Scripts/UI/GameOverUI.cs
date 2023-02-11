using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipesDeliveredText;
        
        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnStateChanged;
            GameInput.Instance.InteractEvent += OnInteract;
            Hide();
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnStateChanged -= OnStateChanged;
            GameInput.Instance.InteractEvent -= OnInteract;
        }

        private void OnInteract(object sender, EventArgs e)
        {
            if (GameManager.Instance.IsGameOver())
            {
                SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
            }
        }

        private void OnStateChanged(object sender, GameManager.State state)
        {
            if (state == GameManager.State.GameOver)
            {
                recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
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
