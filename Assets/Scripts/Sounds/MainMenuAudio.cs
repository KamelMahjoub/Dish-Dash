using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    private AudioSource audioSource;
    
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public float Volume { get; private set; } = 0.2f;
    
    private void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.1f);
        audioSource.volume = Volume;
    }
}
