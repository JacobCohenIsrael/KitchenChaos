using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 5f;

    private bool isWalking;
    
    private void Update()
    {
        var inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;

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
