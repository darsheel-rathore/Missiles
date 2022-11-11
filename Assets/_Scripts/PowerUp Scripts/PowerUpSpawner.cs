using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	[SerializeField] GameObject[] powerUpPrefab;
	ValidatePlayer checkPlayer;

	// Use this for initialization
	void Start () {
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();

        if (checkPlayer.isPlayerAlive())
		{
			StartCoroutine(SpawnShieldPowerUp());
			StartCoroutine(SpawnSpeedPowerUp());
		}
	}

    void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (checkPlayer.isPlayerAlive() == false)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator SpawnShieldPowerUp()
	{
		while(true)
		{
			if(!GameObject.FindGameObjectWithTag("Shield PowerUp"))
			{
				yield return new WaitForSeconds(5f);
				Instantiate(powerUpPrefab[0], gameObject.GetComponent<Boundries>().SetUpBoundry(), Quaternion.identity);
			}
			else
			{
				yield return new WaitForSeconds(1f);
			}
		}
	}

	private IEnumerator SpawnSpeedPowerUp()
	{
		while(true)
		{
			if(!GameObject.FindGameObjectWithTag("Speed PowerUp"))
			{
				yield return new WaitForSeconds(5f);
				Instantiate(powerUpPrefab[1], gameObject.GetComponent<Boundries>().SetUpBoundry(), Quaternion.identity);
			}
			else
			{
				yield return new WaitForSeconds(1f);
			}
		}
	}
}