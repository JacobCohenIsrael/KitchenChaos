using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStartUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private Animator animator;
        [SerializeField] private SoundManager soundManager;

        private int previousCountdownNumber;
        private static readonly int NumberPopupTriggerName = Animator.StringToHash("NumberPopup");

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
            var countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownTimer());
            // CR: Used "N0" format
            countdownText.text = countdownNumber.ToString("N0");

            if (previousCountdownNumber != countdownNumber)
            {
                animator.SetTrigger(NumberPopupTriggerName);
                previousCountdownNumber = countdownNumber;
                soundManager.PlayCountdown();
            }
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
