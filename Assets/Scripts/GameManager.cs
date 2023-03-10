using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameLength = 20f;

    public static GameManager Instance { get; private set; }

    public event EventHandler<State> OnStateChanged;

    // CR: instead of two events, just send the pause state
    public event EventHandler<bool> OnPauseChanged;
    
    public enum State
    {
        Pending,
        Countdown,
        Playing,
        GameOver
    }

    private State state;

    private float countdownTimer = 3f;
    private float gameplayTimer;
    private bool isGamePaused;

    public float GetCountdownTimer()
    {
        return countdownTimer;
    }

    public bool IsGamePlaying()
    {
        return state == State.Playing;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
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

    private void Start()
    {
        GameInput.Instance.OnPauseAction += OnGamePaused;
        GameInput.Instance.InteractEvent += OnInteract;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPauseAction -= OnGamePaused;
        GameInput.Instance.InteractEvent -= OnInteract;
    }
    
    private void OnInteract(object sender, EventArgs e)
    {
        if (state == State.Pending)
        {
            ChangeState(State.Countdown);
        }
    }

    private void OnGamePaused(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        OnPauseChanged?.Invoke(this, isGamePaused);
        if (isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Pending:
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
