using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour {

	ValidatePlayer checkPlayer;
	Player player;
	float tempPlayerSpeed;

	// Use this for initialization
	void Start () {
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
        
        // Check Player is alive or not
        if (checkPlayer.isPlayerAlive())
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		// Store Player Current Speed
		tempPlayerSpeed = player.GetAircraftSpeed();

		// Start Coroutine
		StartCoroutine(ChangePlayerSpeed());
	}

    void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if(checkPlayer.isPlayerAlive() == false)
        {
            Destroy(this.gameObject);
        }
    }

	IEnumerator ChangePlayerSpeed()
	{
		// Change Player Speed
		player.SetAircraftSpeed(tempPlayerSpeed + 5);
		yield return new WaitForSeconds(5f);
		
		// Change Player Speed Back To Normal
		player.SetAircraftSpeed(tempPlayerSpeed);
		Destroy(this.gameObject);
	}
}
