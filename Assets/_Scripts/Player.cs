using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionClip;


    private Rigidbody2D rb;
    private float rotationSpeed = 250f;
    private float aircraftSpeed = 7f;
    CloudsSpawner csScript;
    private float angleToRotate;
    [SerializeField] private bool isReadyToRotate = false;

    // Restore values
    float resetSpeed;

    // Score Manager
    ScoreManager scoreManagerScript;

    // Canvas Manager
    CanvasHandler canvasHandler;

    void Start() {

        rb = gameObject.GetComponent<Rigidbody2D>();
        csScript = FindObjectOfType<CloudsSpawner>();//GameObject.FindGameObjectWithTag("Cloud Spawner").GetComponent<CloudsSpawner>();

        // Setting up resetting values
        resetSpeed = aircraftSpeed;

        // Score Manager
        scoreManagerScript = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();

        // Canvas Manager
        canvasHandler = GameObject.FindGameObjectWithTag("Canvas Handler").GetComponent<CanvasHandler>();
    }


    void Update()
    {
        csScript.SetPlayerSpeed(aircraftSpeed, resetSpeed);
        if (isReadyToRotate)
        {
            HandleResetRotation();
        }

    }

    private void HandleResetRotation()
    {
        transform.Rotate(new Vector3(0, 0, Mathf.Lerp(0, angleToRotate, 0.1f)));
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }


    private void PlayerMovement()
    {
        rb.velocity = transform.up * aircraftSpeed;

        // Variables for touch and deviation value
        float inputX = 0;
        float touchInput = 0;

        // if in-game canvas is active only then player can alter their plane movements
        if (GameObject.FindGameObjectWithTag("InGame Canvas") != null)
        {
            if (Input.touchCount > 0)
            {
                touchInput = Camera.main.ScreenToWorldPoint(
                    new Vector3(
                    Input.GetTouch(0).position.x,
                    Input.GetTouch(0).position.y)).x;

                if (touchInput < transform.position.x)
                {
                    inputX = 1f;
                }
                else if (touchInput >= transform.position.x)
                {
                    inputX = -1f;
                }
            }
            else
            {
                inputX = -Input.GetAxis("Horizontal");
            }

            // Control pitch when plane is deviated
            HandlePitchControl(inputX);
            rb.angularVelocity = inputX * rotationSpeed;
        }
    }

    private void HandlePitchControl(float horizontalInput)
    {
        if (horizontalInput < 0 || horizontalInput > 0)
        {
            GetComponent<AudioSource>().pitch = 1.2f;
        }
        else if (horizontalInput == 0)
        {
            GetComponent<AudioSource>().pitch = 1f;
        }
    }


	// Getter for aircraftSpeed
	public float GetAircraftSpeed()
	{
		return aircraftSpeed;
	}

	// Setter for aircraftSpeed
	public void SetAircraftSpeed(float aircraftSpeed)
	{
		this.aircraftSpeed = aircraftSpeed;
	}

    // Getter for aircraft position
    public Vector3 GetAircraftPosition()
    {
        return transform.position;
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
                // Opening game over canvas
                canvasHandler.GameOverCanvas();

                // Displaying Final Score
                scoreManagerScript.DisplayFinalScore();

                // Run Die Method
                Die();
                break;
        }
    }

    private void Die()
    {
        // Show explosion animation
        GameObject explosionAnimation = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionAnimation, 2f);

        // Play explosion sound
        AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, 1f);

        // Destroy player 
        Destroy(this.gameObject);
    }


    // Resetting Player Position
    public void ResetPositionPlayer()
    {
        angleToRotate = Vector3.Angle(rb.velocity, new Vector2(0, 1));
        if ((rb.velocity.x >= 0 && rb.velocity.y >= 0) || (rb.velocity.x >= 0 && rb.velocity.y < 0))
        {
            angleToRotate *= 1f;
        }
        else
        {
            angleToRotate *= -1f;
        }
        StartCoroutine(RotationRoutine());
    }

    private IEnumerator RotationRoutine()
    {
        isReadyToRotate = true;
        yield return new WaitForSeconds(0.5f);
        isReadyToRotate = false;
    }
}
