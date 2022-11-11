using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

	[Range(1f, 10f)]
	[SerializeField] float rotationSpeed = 3f;

	ValidatePlayer checkForPlayer;
	Transform player;
    ScoreManager scoreManager;
	
	void Start()
	{
		checkForPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Grabbing Score Manager
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
    }

	private void Update()
	{
		transform.Rotate(new Vector3(0, 0, rotationSpeed));
        CheckForPlayer();
	}

    private void CheckForPlayer()
    {
        if (checkForPlayer.isPlayerAlive())
        {
            CheckForDistance();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void CheckForDistance()
	{
        Vector2 directionVector = player.position - transform.position;

        if (directionVector.magnitude > 30f)
        {
            gameObject.GetComponent<AlertSystem>().SelfDestruct();
            SelfDestruct();
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
            scoreManager.StarCollected();
			SelfDestruct();
		}
	}

	private void SelfDestruct()
	{
		Destroy(this.gameObject);
	}

}
