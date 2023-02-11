using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInteractions))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private float footstepTimerMax = 0.1f;
    
        private PlayerInteractions playerInteractions;
        private PlayerMovement playerMovement;
        private float footstepTimer;

        private void Awake()
        {
            // CR: Add "RequiredComponent" to ensure no mistakes were made
            playerInteractions = GetComponent<PlayerInteractions>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            // CR: No need to update timer if player is not walking
            if (!playerMovement.IsWalking()) return;
            footstepTimer -= Time.deltaTime;

            if (footstepTimer < 0f)
            {
                footstepTimer = footstepTimerMax;
            
                soundManager.PlayFootstepSound(playerInteractions.transform, 0.5f);
            }
        }
    }
}
