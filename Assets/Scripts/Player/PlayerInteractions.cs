using System;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour, IKitchenObjectParent
    {
        public event EventHandler OnKitchenItemPicked;
    
        [SerializeField] private GameInput gameInput;
        [SerializeField] private Transform kitchenObjectHoldPoint;
        [SerializeField] private LayerMask counterLayerMask;
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
            HandleInteractions();
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
}
