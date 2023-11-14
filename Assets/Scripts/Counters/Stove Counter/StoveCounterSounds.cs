using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSounds : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {


        if (GameManager.Instance.IsGamePaused())
            audioSource.volume = 0;
        else
            SetVolume();
        
        if (playWarningSound & GameManager.Instance.IsGamePlaying)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }


        
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state is StoveCounter.State.Frying or StoveCounter.State.Fried;

        if (playSound && GameManager.Instance.IsGamePlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }


    private void SetVolume()
    {
        audioSource.volume = SoundManager.Instance.Volume/2;
    }
}