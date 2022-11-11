using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageAudioSprite : MonoBehaviour {

    [SerializeField] Sprite[] muteUnmuteSprite;

    private Image imageComponent;
    
    void Start()
    {
        imageComponent = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update () {
        ManageSprites();
    }

    private void ManageSprites()
    {
        if (AudioListener.volume == 0f)
        {
            // show mute audio sprite
            imageComponent.sprite = muteUnmuteSprite[0];
        }
        else if (AudioListener.volume == 1f)
        {
            // show Unmute audio sprite
            imageComponent.sprite = muteUnmuteSprite[1];
        }
    }
}
