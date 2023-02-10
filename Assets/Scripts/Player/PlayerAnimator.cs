using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private PlayerMovement playerMovement;
    
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private void Awake()
        {
            animator.SetBool(IsWalking, playerMovement.IsWalking());
        }

        private void Update()
        {
            animator.SetBool(IsWalking, playerMovement.IsWalking());
        }
    }
}