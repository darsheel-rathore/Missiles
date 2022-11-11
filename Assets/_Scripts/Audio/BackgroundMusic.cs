using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private AudioSource audioSource;
    private float currentVolume;
   [SerializeField] private BackgroundMusicSprite backgroundMusicSpriteScript;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentVolume = audioSource.volume;
    }

    public void ManageBackgroundMusic()
    {
        if(audioSource.volume <= currentVolume && audioSource.volume > 0f)
        {
            audioSource.volume = 0f;

            // Change the existing sprite to mute audio sprite
            backgroundMusicSpriteScript.Mute();
        }
        else
        {
            audioSource.volume = currentVolume;

            // Change the existing sprite to unmute audio sprite
            backgroundMusicSpriteScript.Unmute();
        }
    }
}
