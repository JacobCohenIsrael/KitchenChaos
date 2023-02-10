using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
    }

    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.State state)
    {
        var playSound = state is StoveCounter.State.Frying or StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();;
        }
        
    }
}
