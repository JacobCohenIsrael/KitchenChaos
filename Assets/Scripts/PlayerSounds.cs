using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private float footstepTimerMax = 0.1f;
    
    private Player player;
    private float footstepTimer;

    private void Awake()
    {
        // CR: Add "RequiredComponent" to ensure no mistakes were made
        player = GetComponent<Player>();
    }

    private void Update()
    {
        // CR: No need to update timer if player is not walking
        if (!player.IsWalking()) return;
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            
            soundManager.PlayFootstepSound(player.transform, 0.5f);
        }
    }
}
