using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private GameInput gameInput;

        private bool isWalking;

        private void Update()
        {
            HandleMovement();
        }
        
        public bool IsWalking()
        {
            return isWalking;
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
    }
}
