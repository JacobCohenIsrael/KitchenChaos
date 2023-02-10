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
            Hide();
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnStateChanged -= OnStateChanged;
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
