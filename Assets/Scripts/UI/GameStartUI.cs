using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStartUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnStateChanged;
            Hide();
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnStateChanged -= OnStateChanged;
        }

        private void Update()
        {
            // CR: Used "N0" format
            countdownText.text = GameManager.Instance.GetCountdownTimer().ToString("N0");
        }

        private void OnStateChanged(object sender, GameManager.State state)
        {
            if (state == GameManager.State.Countdown)
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
