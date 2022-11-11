using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour {

	ValidatePlayer checkPlayer;
	[SerializeField] GameObject starPrefab;
	
	void Start()
	{
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
	}

	void Update()
	{
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (checkPlayer.isPlayerAlive())
        {
            SpawnStar();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SpawnStar()
	{
		while(!GameObject.FindGameObjectWithTag("Star"))
		{
			Vector2 insPosition = gameObject.GetComponent<Boundries>().SetUpBoundry();
			Instantiate(starPrefab, insPosition, Quaternion.identity);
		}
	}

	

}
