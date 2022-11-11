using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	ValidatePlayer checkPlayer;
	float minDistanceForDestroy = 20f;

	void Start()
	{
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
	}

	void Update()
	{
		if(checkPlayer.isPlayerAlive())
		{
			Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
			Vector2 distanceVector = playerTransform.position - transform.position;
			float distanceWithPlayer = distanceVector.magnitude;
			
			if (distanceWithPlayer >= minDistanceForDestroy)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
