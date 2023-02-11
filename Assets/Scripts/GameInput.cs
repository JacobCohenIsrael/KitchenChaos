using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler InteractEvent;
    public event EventHandler AltInteractEvent;

    public event EventHandler OnPauseAction;
    
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        playerInputActions.Player.Interact.performed += InteractPerformed;
        playerInputActions.Player.AltInteract.performed += AltInteractPerformed;
        playerInputActions.Player.Pause.performed += PausePerformed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractPerformed;
        playerInputActions.Player.AltInteract.performed -= AltInteractPerformed;
        playerInputActions.Player.Pause.performed -= PausePerformed;
        
        playerInputActions.Dispose();
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void AltInteractPerformed(InputAction.CallbackContext callbackContext)
    {
        AltInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPerformed(InputAction.CallbackContext callbackContext)
    {
        InteractEvent?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedMovementVector()
    {
        var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
