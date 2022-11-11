using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicSprite : MonoBehaviour {

    [SerializeField] Sprite[] backgroundMusicSprites;

    public void Mute()
    {
        GetComponent<Image>().sprite = backgroundMusicSprites[0];
    }

    public void Unmute()
    {
        GetComponent<Image>().sprite = backgroundMusicSprites[1];
    }
}
