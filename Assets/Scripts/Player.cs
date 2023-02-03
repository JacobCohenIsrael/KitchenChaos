using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [FormerlySerializedAs("layerMask")] [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.InteractEvent += OnInteract;
    }

    private void OnInteract(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
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
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (selectedCounter != clearCounter)
                {
                    selectedCounter = clearCounter;
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
            canMove = !Physics.CapsuleCast(transformPosition, transformPosition + Vector3.up * playerHeight, playerRadius,
                moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transformPosition, transformPosition + Vector3.up * playerHeight,
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

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
