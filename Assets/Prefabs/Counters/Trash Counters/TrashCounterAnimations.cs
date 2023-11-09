using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterAnimations : MonoBehaviour
{
    [SerializeField] private TrashCounter trashCounter;
    private Animator animator;

    private const string OPEN_LID = "OpenLid";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        trashCounter.OnLidOpen += TrashCounter_OnPlayerOpenLid;   
    }


 
    private void TrashCounter_OnPlayerOpenLid(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_LID);
    }
}