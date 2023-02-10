using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public event EventHandler OnKitchenItemPicked;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        gameInput.InteractEvent += OnInteract;
        gameInput.AltInteractEvent += OnAltInteract;
    }

    private void OnDestroy()
    {
        gameInput.InteractEvent -= OnInteract;
        gameInput.AltInteractEvent -= OnAltInteract;
    }

    private void OnInteract(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void OnAltInteract(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.AltInteract(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        var inputVector = gameInput.GetNormalizedMovementVector();

        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        const float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        
        if (Physics.Raycast(transform.position, lastInteractDir, out var raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter counter))
            {
                if (selectedCounter != counter)
                {
                    if (selectedCounter != null)
                    {
                        selectedCounter.DeSelect();
                    }
                    selectedCounter = counter;
                    selectedCounter.Select();
                }
            }
            else
            {
                if (selectedCounter != null)
                {
                    selectedCounter.DeSelect();
                    selectedCounter = null;
                }
            }
        }
        else
        {
            if (selectedCounter != null)
            {
                selectedCounter.DeSelect();
                selectedCounter = null;
            }
        }
    }

    private void HandleMovement()
    {
        var inputVector = gameInput.GetNormalizedMovementVector();

        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = .7f;
        var playerHeight = 2f;
        var transformPosition = transform.position;
        var canMove = !Physics.CapsuleCast(transformPosition, transformPosition + Vector3.up * playerHeight, playerRadius,
            moveDir, moveDistance);

        if (!canMove)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDirX.x != 0 && !Physics.CapsuleCast(transformPosition, transformPosition + Vector3.up * playerHeight, playerRadius,
                moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDirX.z != 0 && !Physics.CapsuleCast(transformPosition, transformPosition + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        if (isWalking)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnKitchenItemPicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
