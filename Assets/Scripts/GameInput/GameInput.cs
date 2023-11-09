using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        
        Instance = this; 
        
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
        
        playerInputActions.Player.Interaction.performed += InteractPerformed;
        
        playerInputActions.Player.AlternateInteraction.performed += InteractAlternatePerformed;
        
        playerInputActions.Player.Pause.performed += PausePerformed; 
    }
    

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
    
    
    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    private void InteractAlternatePerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    
    
    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    
    //Unsubscribing from events whenever the player goes back to the main menu 
    private void OnDestroy()
    {
        playerInputActions.Player.Interaction.performed -= InteractPerformed;
        
        playerInputActions.Player.Pause.performed -= PausePerformed;  
        
        playerInputActions.Dispose();
    }
}