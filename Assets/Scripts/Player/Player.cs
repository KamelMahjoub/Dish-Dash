using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectHolder
{
    public static Player Instance { get; private set; }


    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private BaseCounter selectedCounter;

    private Vector3 lastInteractDirection;
    
    //the kitchen object on top of the counter
    private KitchenObject kitchenObject;
    
    [SerializeField] private Transform kitchenObjectHoldPoint;
    public bool IsWalking { private set; get; }

    private void Awake()
    {
        //singleton
        if (Instance != null)
        {
            Debug.LogError("There is more than one player instance");
        }
        Instance = this;
    }
    

    private void Update()
    {
       HandleMovement();
       HandleInteractions();
    }
 
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float movementDistance = Time.deltaTime * movementSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, movementDistance);

        
        if (!canMove)
        {
            //Cannot move towards move direction
            //Attempt only X movement
            // normalized so that it matches the normal speed.
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x is < -0.5f or > 0.5f)  && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, movementDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX;  
            }
            else
            {
                //cannot move on x
                //attempt Z movement
                // normalized so that it matches the normal speed.
                Vector3 moveDirectionZ = new Vector3(0, 0 , moveDirection.z).normalized;
                canMove = (moveDirection.z is < -0.5f or > 0.5f) && !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, movementDistance);
                
                if (canMove)
                    moveDirection = moveDirectionZ;
                else
                {
                    //Cant move anywhere
                }
            }
        }
        
        if(canMove)
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

        IsWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 10f;
        
        transform.forward = Vector3.Slerp(transform.forward,moveDirection,Time.deltaTime * rotateSpeed);
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        //keep checking for interaction when stopped moving
        if (moveDirection != Vector3.zero)
            lastInteractDirection = moveDirection;
        
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //has clear counter
                if (baseCounter != selectedCounter)
                { 
                   SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { 
            selectedCounter =  selectedCounter
        });
    }

    
    public KitchenObject GetKitchenObject() => kitchenObject;
  
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        
        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
        
    }
  
    public bool HasKitchenObject() => kitchenObject != null;
  
    public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;
  
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    
    
}
