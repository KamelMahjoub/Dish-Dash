using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashing : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private const string IS_FLASHING= "IsFlashing";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING,false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
      
        animator.SetBool(IS_FLASHING,show);
    }
}
