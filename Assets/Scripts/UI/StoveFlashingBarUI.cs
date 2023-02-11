using Counters;
using UnityEngine;

namespace UI
{
    public class StoveFlashingBarUI : MonoBehaviour
    { 
        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private float burnAlertThreshold = 0.5f;
        [SerializeField] private Animator animator;
        private static readonly int IsFlashing = Animator.StringToHash("IsFlashing");

        private void Start()
        {
            animator.SetBool(IsFlashing, false);
            stoveCounter.OnProgressChanged += OnProgressChanged;
        }

        private void OnProgressChanged(object sender, float progress)
        {
            var show = stoveCounter.IsFried() && progress >= burnAlertThreshold;

            animator.SetBool(IsFlashing, show);
        }
    }
}
