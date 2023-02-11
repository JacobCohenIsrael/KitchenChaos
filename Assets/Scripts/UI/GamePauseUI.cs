using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
    }

    private void OnResumeButtonClicked()
    {
        GameManager.Instance.TogglePauseGame();
    }

    private void Start()
    {
        Hide();
        GameManager.Instance.OnPauseChanged += OnPauseChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPauseChanged -= OnPauseChanged;
    }

    private void OnPauseChanged(object sender, bool isPaused)
    {
        if (isPaused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
