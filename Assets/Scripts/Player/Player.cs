using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static Player Instance { get; private set; }
    
    [SerializeField] private float movementSpeed = 7f;
    
    [SerializeField] private GameInput gameInput;
    
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
    
}
