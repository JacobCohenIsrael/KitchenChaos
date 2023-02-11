using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int Cut = Animator.StringToHash("Cut");

    public void PlayCut()
    {
        animator.SetTrigger(Cut);
    }
}
