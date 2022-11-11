using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {


	Player player;
	ValidatePlayer checkPlayer;
	[SerializeField] float timeBeforeDestruct = 5f;


	// Use this for initialization
	void Start () {
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
		if (checkPlayer.isPlayerAlive())
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		// Calling Self destruct
		StartCoroutine(SelfDestruct());
	}

	
	// Update is called once per frame
	void Update () {
		if (checkPlayer.isPlayerAlive())
		{
			UpdatePosition();
		}	
	}

	void UpdatePosition()
	{
		transform.position = player.transform.position;
	}


	private IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(timeBeforeDestruct);
		Destroy(this.gameObject);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "HM1" || other.tag == "FlyBy" || other.tag == "TargetPrediction" || other.tag == "HM2" || other.tag == "HM3")
		{
			Destroy(this.gameObject);
		}
	}

}
