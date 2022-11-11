using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour {

    [SerializeField] GameObject shieldPrefab;
    private ValidatePlayer checkPlayer;
	private Player playerScript;

	void Start()
	{
        checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
		if (checkPlayer.isPlayerAlive())
		{
			playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
	}

    void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if(checkPlayer.isPlayerAlive() == false)
        {
            GetComponent<AlertSystem>().SelfDestruct();
            Destroy(this.gameObject);
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{			
			if(checkPlayer.isPlayerAlive())
			{
				SpawnShield();
			}
			Destroy(this.gameObject);
		}
	}


	private void SpawnShield()
	{
		Instantiate(shieldPrefab, playerScript.transform.position, Quaternion.identity);
	}

}
