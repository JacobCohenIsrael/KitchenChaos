using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler InteractEvent;
    public event EventHandler AltInteractEvent;
    
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        playerInputActions.Player.Interact.performed += InteractPerformed;
        playerInputActions.Player.AltInteract.performed += AltInteractPerformed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractPerformed;
        playerInputActions.Player.AltInteract.performed -= AltInteractPerformed;
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
