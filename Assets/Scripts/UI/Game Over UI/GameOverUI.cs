using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake()
    {
        mainMenuButton.onClick.AddListener((() =>
        { 
            Loader.Load(Loader.Scene.MainMenu);
        }));
      
        restartButton.onClick.AddListener((() =>
        { 
            Loader.Load(Loader.Scene.GameScene);
        }));
    }


    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; 
        Hide();
    }
   
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.SuccessfulRecipes.ToString();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        gameObject.SetActive(true);
        restartButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
