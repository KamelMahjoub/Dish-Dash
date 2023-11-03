using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    
    private const string IsWalking = "IsWalking";
    private const string IsCarrying = "IsCarrying";
    private const string IsGrabbingKnife = "IsGrabbingKnife";
    private const string IsCutting = "IsCutting";
    private const string IsPuttingDownKnife = "IsPuttingDownKnife";
    
    private Animator animator;
    [SerializeField] private Player player;
    
    [SerializeField] private GameObject playerKnife;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();

        player.OnCut += Player_OnCutPerformed;
    }

    private void Player_OnCutPerformed(object sender, EventArgs e)
    {
        Player.Instance.IsCutting = true;
        Player.Instance.CanMove = false;
        Player.Instance.CanInteract = false;
        
        playerKnife.SetActive(true);
        
        PerformGrabAnimation();
        PerformCutAnimation();
        PerformPutBackAnimation();
        
        StartCoroutine(Cooldown());        
        
    }

    private void Update()
    {
        animator.SetBool(IsWalking , player.IsWalking);
        animator.SetBool(IsCarrying , player.IsCarrying);
    }
    
    
    private void PerformGrabAnimation()
    {
        animator.SetTrigger(IsGrabbingKnife);
    }
    
    private void PerformCutAnimation()
    {
        animator.SetTrigger(IsCutting);
    }
    
    private void PerformPutBackAnimation()
    {
        animator.SetTrigger(IsPuttingDownKnife);
    }
    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.95f);
        Player.Instance.IsCutting = false;
        Player.Instance.CanMove = true;
        Player.Instance.CanInteract = true;
        playerKnife.SetActive(false);
    }
    
    
}
