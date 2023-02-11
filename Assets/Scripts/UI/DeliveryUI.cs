using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeliveryUI : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Image background;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI messageText;

        // CR: Scriptable Object CLASSIC!
        [SerializeField] private Color successColor;
        [SerializeField] private Color failureColor;
        
        [SerializeField] private Sprite successSprite;
        [SerializeField] private Sprite failureSprite;
        private static readonly int Popup = Animator.StringToHash("Popup");

        private void Start()
        {
            DeliveryManager.Instance.OnDeliverySuccess += OnDeliverySuccess;
            DeliveryManager.Instance.OnDeliveryFail += OnDeliveryFail;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            DeliveryManager.Instance.OnDeliverySuccess -= OnDeliverySuccess;
            DeliveryManager.Instance.OnDeliveryFail -= OnDeliveryFail;
        }

        private void OnDeliveryFail(object sender, EventArgs e)
        {
            gameObject.SetActive(true);
            background.color = failureColor;
            iconImage.sprite = failureSprite;
            messageText.text = "DELIVERY\nFAILED";
            animator.SetTrigger(Popup);
        }

        private void OnDeliverySuccess(object sender, EventArgs e)
        {
            gameObject.SetActive(true);
            background.color = successColor;
            iconImage.sprite = successSprite;
            messageText.text = "DELIVERY\nSUCCESS";
            animator.SetTrigger(Popup);
        }
    }
}
