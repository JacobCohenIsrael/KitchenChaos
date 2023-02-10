using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerInteractions))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private PlayerInteractions playerInteractions;
        
        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerInteractions = GetComponent<PlayerInteractions>();

            playerMovement.enabled = false;
            playerInteractions.enabled = false;
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnStateChanged -= OnGameStateChanged;

        }

        private void OnGameStateChanged(object sender, GameManager.State state)
        {
            switch (state)
            {
                case GameManager.State.Pending:
                    break;
                case GameManager.State.Countdown:
                    playerMovement.enabled = true;
                    break;
                case GameManager.State.Playing:
                    playerInteractions.enabled = true;
                    break;
                case GameManager.State.GameOver:
                    playerInteractions.enabled = false;
                    playerMovement.enabled = false;
                    break;
            }
        }
    }
}
