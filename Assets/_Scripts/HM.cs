using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM : MonoBehaviour {

    // Config Params
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionClip;

    private Rigidbody2D rb;
	private ValidatePlayer checkPlayer;
	private Transform player;

	private float HMSpeed = 8f;
	private float rotationSpeed = 150f;

    ScoreManager scoreManager;


    // Trail Variables
    [SerializeField] private GameObject smokeTrailPrefab;
	GameObject smokeTrail;


	// Use this for initialization
	void Start () 
	{
		checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		rb = gameObject.GetComponent<Rigidbody2D>();

		// Checking for missile type
		CheckForMissileType();

		// Instantiate Smoke Trail
		SpawnTrailEffect();

        // Grabbing Score Manager
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();

        // Starting Death Coroutine
        StartCoroutine(DeathRoutine());
    }


    void Update()
	{
		// Trail Effect Update Position and rotation
		TrailEffectRotationAndPosition();
	}


	// Trail effect methods
	private void SpawnTrailEffect()
	{
		smokeTrail = Instantiate(smokeTrailPrefab, transform.position - new Vector3(0, 1f, 0), Quaternion.identity) as GameObject;
	}

	void TrailEffectRotationAndPosition()
	{
		smokeTrail.GetComponent<TrailEffect>().UpdatePosition(transform.position);
		smokeTrail.GetComponent<TrailEffect>().UpdateAngularVelocity(rb.angularVelocity);
	}
	// ==== //

	private void CheckForMissileType()
	{
		switch(this.tag)
		{
			case "HM1":
				HMSpeed = 8f;
				rotationSpeed = 200f;
				break;

			case "HM2":
				HMSpeed = 9f;
				rotationSpeed = 200f;
				break;

			case "HM3":
				HMSpeed = 11f;
				rotationSpeed = 220f;
				break;

			case "HM4":
				HMSpeed = 12f;
				rotationSpeed = 220f;
				break;	
		}
	}
    
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(checkPlayer.isPlayerAlive()) 
		{
			Movement();
		}
        else
        {
            GetComponent<AlertSystem>().SelfDestruct();
            Die();
        }

	}
	

	private void Movement()
	{
		rb.velocity = transform.up * HMSpeed;

		// Find Direction Vector    
		Vector3 directionVector = player.transform.position - rb.transform.position;

        // Calling flyby sound method using direction vector
        ControlFlyBySound(directionVector.magnitude);

        // Normalizing Vector
        directionVector.Normalize();

		float rotionAngle = Vector3.Cross(directionVector, transform.up).z;

		// Set rotaion angle
		rb.angularVelocity = -rotionAngle * rotationSpeed;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case "HM1":

			case "HM2":

			case "HM3":

			case "HM4":

			case "TargetPrediction":

			case "FlyBy":
                scoreManager.MissileBonusEarned();
                Die();
                break;

			case "Player":

			case "Shield":
                Die();
				break;
		}
	}

    // Volume control
    private void ControlFlyBySound(float distanceFromPlayer)
    {
        if (distanceFromPlayer <= 10f)
        {
            GetComponent<AudioSource>().volume = (float) 1 - (distanceFromPlayer / 10);
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }

    private void Die()
    {
        // Show Missile Explosion
        GameObject explosionAnimation = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionAnimation, 1.5f);

        // Play explosion sound
        AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, 0.5f);

        // Destroy the Missile
        GetComponent<AlertSystem>().SelfDestruct();
        Destroy(this.gameObject);
    }

    // Start Death Life
    private IEnumerator DeathRoutine()
    {
        float deathTime = 0f;
        if (this.tag == "HM1")
        {
            deathTime = 12f;
        }
        else
        {
            deathTime = 8f;
        }
        yield return new WaitForSeconds(deathTime);
        Die();
    }
}
