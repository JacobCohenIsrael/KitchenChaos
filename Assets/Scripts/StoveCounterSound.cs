using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private float burnAlertThreshold = 0.5f;

    private float warningSoundTimer;
    private bool playWarningSound;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
        stoveCounter.OnProgressChanged += OnProgressChanged;
    }

    private void OnProgressChanged(object sender, float progress)
    {
        playWarningSound = stoveCounter.IsFried() && progress >= burnAlertThreshold;
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

    private void Update()
    {
        if (!playWarningSound) return;
        
        warningSoundTimer -= Time.deltaTime;
        if (warningSoundTimer <= 0f)
        {
            warningSoundTimer = 0.2f;
            
            SoundManager.Instance.PlayWarningSound(stoveCounter.transform);
        }
    }
}
