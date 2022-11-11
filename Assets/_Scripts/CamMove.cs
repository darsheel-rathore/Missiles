using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

	private GameObject aircraft;
	private float newDeltaX, newDeltaY;
	private ValidatePlayer checkPlayer;

    //float min = 0f;
    //float max = 1f;
    //float interpolateValue = 0.0f;
    
	
	
	void Start()
	{
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
	}

	void Update () {

		if(checkPlayer.isPlayerAlive()) 
		{
			CamMovement();
		}
        
        //Color newColor = new Color(Mathf.Lerp(0.3f, 1f, interpolateValue), Mathf.Lerp(0.3f, 0.6f, interpolateValue), Mathf.Lerp(0.4f, 0.8f, interpolateValue), 1);
     
        //GetComponent<Camera>().backgroundColor = newColor;

        //interpolateValue += 0.05f * Time.deltaTime;

	}


    private void CamMovement()
    {
		aircraft = GameObject.FindGameObjectWithTag("Player");

		newDeltaX = aircraft.transform.position.x;
		newDeltaY = aircraft.transform.position.y;
        transform.position = new Vector3(newDeltaX, newDeltaY, transform.position.z);
    }
}
