using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPrediction : MonoBehaviour {

	private Rigidbody2D rb;
	private ValidatePlayer checkPlayer;
	private Transform playerTransform;
	private Rigidbody2D playerRB;
	Vector2 estimatedPos;
	private float targetPredictionSpeed;
	private bool grabPosOnce = true;
	private float timeToCollide = 1.5f;

    ScoreManager scoreManager;

    // Deviation Variables
    float minX = -5f, maxX = 5f, minY = -5f, maxY = 5f;
	float newDeltaX, newDeltaY;


	[SerializeField] private GameObject smokeTrailPrefab;
	GameObject smokeTrail;


	// Use this for initialization
	void Start ()
    {
        checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        // Setting Variation for flyBy missiles
        SettingRange();


		// Instantiate Smoke Trail
		SpawnTrailEffect();

        // Grabbing Score Manager
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();

        // Starting Death Coroutine
        StartCoroutine(DeathRoutine());
    }


    // Update is called once per frame
    void Update () {
		if (checkPlayer.isPlayerAlive())
		{
			if (grabPosOnce == true)
			{
				// Get possible pos
				FlyForTarget();
				grabPosOnce = false;
			}
		}

		// Trails effects
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
	// === //


	    private void SettingRange()
    {
        newDeltaX = Random.Range(minX, maxX);
        newDeltaY = Random.Range(minY, maxY);
    }

	void FlyForTarget()
	{
		// Calculating estimated position after certain time
		estimatedPos = (Vector2)playerTransform.position + (playerRB.velocity * timeToCollide);

		// Checking for Missile Type
		CheckForMissileType();

		// Calculating direction to fire in
		Vector2 directionToFire = estimatedPos - (Vector2)transform.position;

		// Calling rotation method
		RotatingTowardsTarget(directionToFire);

		// Calculating distance to cover
		float distanceToCollision = directionToFire.magnitude;

		// Normalizing the direction vector
		directionToFire.Normalize();

		// Calculating speed and puttin in the final velocity
		targetPredictionSpeed = distanceToCollision / timeToCollide;
		rb.velocity = directionToFire * targetPredictionSpeed;
	}

	void RotatingTowardsTarget(Vector2 dir)
	{
		float angleToRotate = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis(angleToRotate, transform.forward);
	}

	private void CheckForMissileType()
	{
		if (this.tag == "FlyBy")
		{
			estimatedPos += new Vector2(newDeltaX, newDeltaY);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "HM1":

            case "HM2":

            case "HM3":

            case "HM4":

            case "TargetPrediction":

            case "FlyBy":
                scoreManager.MissileBonusEarned();
                Destroy(this.gameObject);
                break;

            case "Player":

            case "Shield":
                Destroy(this.gameObject);
                break;
        }
    }


    // Start Death Life
    private IEnumerator DeathRoutine()
    {
        float deathTime = 0f;
        yield return new WaitForSeconds(deathTime);

        // Destroy the Missile
        GetComponent<AlertSystem>().SelfDestruct();
        Destroy(this.gameObject);
    }

}
