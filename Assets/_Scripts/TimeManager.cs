using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI timeField;
    private float currentTime;
    private float updatedTime = 0f;
    private int seconds = 0;
    private int minutes = 0;

    public void StartTimer()
    {
        currentTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        updatedTime = Time.time - currentTime;

        minutes = (int)updatedTime / 60;
        seconds = (int)updatedTime % 60;

        DisplayTime();
    }

    private void DisplayTime()
    {
        // This if only runs at 0:60, 1:60, 2:60
        if (seconds % 60 == 0)
        {
            string minute = "0" + (minutes).ToString();
            string second = ":00";

            timeField.text = minute + second;
        }
        else
        {
            if (minutes < 10 && seconds < 10)
            {
                timeField.text = "0" + minutes.ToString() + ":" + "0" + seconds.ToString("F0");
            }
            else if (seconds < 10)
            {
                timeField.text = minutes.ToString() + ":" + "0" + seconds.ToString("F0");
            }
            else if (minutes < 10)
            {
                timeField.text = "0" + minutes.ToString() + ":" + seconds.ToString("F0");
            }
            else
            {
                timeField.text = minutes.ToString() + ":" + seconds.ToString("F0");
            }
        }
    }

    // Getter for time played
    public int GetPlayTime()
    {
        return (int)updatedTime;
    }

}
