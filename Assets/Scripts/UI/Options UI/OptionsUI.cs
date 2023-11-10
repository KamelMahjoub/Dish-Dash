using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;


    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;


    private Action OnCloseButtonAction;


    private void Awake()
    {
        Instance = this;


        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            OnCloseButtonAction();
        });
    }


    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.Volume * 10f);
        musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.Volume * 10f);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.OnCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}