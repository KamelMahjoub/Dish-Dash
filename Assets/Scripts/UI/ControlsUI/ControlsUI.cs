using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button resumeButton;
    
    [SerializeField] private GameObject pauseMenu;
    
 
    private Action OnCloseButtonAction;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            Close();
        });
        
        
    }

    private void Update()
    {
        closeButton.Select();
        Debug.Log("dd");
    }

    private void Start()
    {
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Close()
    {
        pauseMenu.SetActive(true);
        resumeButton.Select();
    }
    
   

}
