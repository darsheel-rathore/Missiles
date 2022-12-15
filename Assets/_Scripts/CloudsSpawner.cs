using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour {

	// Config Params
	[SerializeField] GameObject[] clouds;
	ValidatePlayer checkPlayer;

	// State Variables
	private float timeBetweenClouds = 2f;
	private float resetTime;
	private float playerNewSpeed = 1, playerInitialSpeed = 1;

	void Start()
	{
		checkPlayer = FindObjectOfType<ValidatePlayer>();
		StartCoroutine(SpawnClouds());

		// Setting Reset time
		resetTime = timeBetweenClouds;
	}


	void Update()
	{
		UpdateTimeForClouds();
	}
	
	IEnumerator SpawnClouds()
	{
		int switchCaseIndex = 1;

		while (true)
		{
			if(checkPlayer.isPlayerAlive()) 
			{	
				while (switchCaseIndex <= 4)
				{
					float xRandom, yRandom;

					switch (switchCaseIndex)
					{
						case 1:
							// up
							xRandom = UnityEngine.Random.Range(0f, 1f);
							yRandom = UnityEngine.Random.Range(1.1f, 1.2f);
							CloudsCreation(xRandom, yRandom);
							break;

						case 2:
							// Right
							xRandom = UnityEngine.Random.Range(1.1f, 1.2f);
							yRandom = UnityEngine.Random.Range(0f, 1.1f);
							CloudsCreation(xRandom, yRandom);
							break;

						case 3:
							// left
							xRandom = UnityEngine.Random.Range(-0.1f, -0.2f);
							yRandom = UnityEngine.Random.Range(0f, 1.1f);
							CloudsCreation(xRandom, yRandom);
							break;

						case 4:
							// Down
							xRandom = UnityEngine.Random.Range(0f, 1f);
							yRandom = UnityEngine.Random.Range(-0.1f, -0.2f);
							CloudsCreation(xRandom, yRandom);
							break;
					}
					switchCaseIndex++;
				}
			}
			else { yield return null; }

			if (switchCaseIndex >= 4) { switchCaseIndex = 1; }
			yield return new WaitForSeconds(timeBetweenClouds);
		}
	}

	private void CloudsCreation(float xRandom, float yRandom)
	{
		Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(xRandom, yRandom, 10));

		Instantiate(
			clouds[UnityEngine.Random.Range(0, 5)],
			position,
			Quaternion.identity
		  );
	}



	// Setters for player timing and time management methods
	public void SetPlayerSpeed(float Updatedspeed, float initSpeed)
	{
		playerNewSpeed = Updatedspeed;
		playerInitialSpeed = initSpeed;
	}

	private void UpdateTimeForClouds()
	{
		timeBetweenClouds = (playerInitialSpeed * resetTime) / playerNewSpeed;
	}



}
