using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button controlsButton;

    [SerializeField] private GameObject controlsUI;


    private void Awake()
    {
        resumeButton.onClick.AddListener((() =>
        {
            GameManager.Instance.TogglePauseGame();
        }));
        
        mainMenuButton.onClick.AddListener((() =>
        { 
            Loader.Load(Loader.Scene.MainMenu);
        }));
        
        optionsButton.onClick.AddListener((() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        }));
        
        controlsButton.onClick.AddListener((() =>
        {
            Hide();
            controlsUI.SetActive(true);
        }));

    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
