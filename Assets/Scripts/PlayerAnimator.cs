using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Player player;
    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private void Awake()
    {
        animator.SetBool(IsWalking, player.IsWalking());
    }

    private void Update()
    {
        animator.SetBool(IsWalking, player.IsWalking());
    }
}