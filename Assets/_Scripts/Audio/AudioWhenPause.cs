using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioWhenPause : MonoBehaviour {

    private AudioSource audioSource;

    float currentVolume;
   
    bool x = false;
    bool y = true;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleAudioWhenPause();
    }

    public void HandleAudioWhenPause()
    {
        if (Time.timeScale == 0)
        {
            if (y == true)
            {
                currentVolume = audioSource.volume;
                audioSource.volume = 0;
                y = false;
                x = true;
            }

        }
        else
        {
            if (x == true)
            {
                audioSource.volume = currentVolume;
                x = false;
                y = true;
            }
        }
    }
}
