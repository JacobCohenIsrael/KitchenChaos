using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    public void PlayOpenClose()
    {
        animator.SetTrigger(OpenClose);
    }
}
