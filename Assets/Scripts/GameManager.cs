using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameLength = 20f;

    public static GameManager Instance { get; private set; }

    public event EventHandler<State> OnStateChanged;
    
    public enum State
    {
        Pending,
        Countdown,
        Playing,
        GameOver
    }

    private State state;

    private float startTimer = 1f;
    private float countdownTimer = 3f;
    private float gameplayTimer = 0f;

    public float GetCountdownTimer()
    {
        return countdownTimer;
    }

    public float GetTimeLeftNormalized()
    {
        return 1 - (gameplayTimer / gameLength);
    }
    
    private void ChangeState(State newState) 
    {
        if (state != newState)
        {
            state = newState;
            OnStateChanged?.Invoke(this, state);
        }    
    }
    
    private void Awake()
    {
        state = State.Pending;
        Instance = this;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Pending:
                startTimer -= Time.deltaTime;
                if (startTimer <= 0)
                {
                    ChangeState(State.Countdown);
                }

                break;
            case State.Countdown:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer <= 0)
                {
                    ChangeState(State.Playing);
                }
                break;
            case State.Playing:
                gameplayTimer += Time.deltaTime;
                if (gameplayTimer >= gameLength)
                {
                    ChangeState(State.GameOver);
                }

                break;
            case State.GameOver:
                break;
        }
    }
}
