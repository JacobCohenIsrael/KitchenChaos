using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetNormalizedMovementVector();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        var playerTransform = transform;
        playerTransform.forward = Vector3.Slerp(playerTransform.forward, moveDir, Time.deltaTime * rotateSpeed);
        playerTransform.position += moveDir * Time.deltaTime * moveSpeed;

        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
