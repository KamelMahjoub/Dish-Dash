using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    
    private AudioSource audioSource;

    public float Volume { get; private set; } = 0.2f;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        
        Volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.1f);
        audioSource.volume = Volume;
    }
    
    
    
    
    public void ChangeVolume()
    {
        Volume += 0.1f;

        if (Volume > 1f)
        {
            Volume = 0f;
        }

        audioSource.volume = Volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME,Volume);
        PlayerPrefs.Save();
    }
}
