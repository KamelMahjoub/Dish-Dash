using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string IsWalking = "IsWalking";
    private const string IsCarrying = "IsCarrying";
    
    private Animator animator;
    [SerializeField] private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IsWalking , player.IsWalking);
        animator.SetBool(IsCarrying , player.IsCarrying);
    }
}
