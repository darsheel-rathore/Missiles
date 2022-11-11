using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSystem : MonoBehaviour {

	[Header("Alert System")]
	[SerializeField] GameObject AlertPrefab;

	Transform playerTransform;
	private string quadrantCase;
	private GameObject alertSignal;

	private SpriteRenderer spriteRendererForAlerts;
    private ValidatePlayer checkPlayer;
    private bool isInstantiated = false;


	void Start()
	{
		spriteRendererForAlerts = GetComponent<SpriteRenderer>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
    }


    void Update()
	{
        if(checkPlayer.isPlayerAlive())
        {
            CheckIncomingDirection();
            AlertSignalHandling();
        }
        else
        {
            SelfDestruct();
        }

     //   CheckForMissileExistance();
    }


    // Check for missile Existance
    //private void CheckForMissileExistance()
    //{
    //    if (this.gameObject == null)
    //    {
    //        SelfDestruct();
    //    }
    //}
    

    // Self Destruct
    public void SelfDestruct()
	{
        if (alertSignal != null)
        {
            Destroy(alertSignal.gameObject);
        }
	}


	// Checking Incoming Direction
	private void CheckIncomingDirection()
	{
		Vector3 direction = playerTransform.position - transform.position;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		if (angle >= -60f && angle < 60f)
			quadrantCase = "x = 0";
		else if (angle >= 60f && angle < 120f)
			quadrantCase = "y = 0";
		else if (angle < -60f && angle >= -120f)
			quadrantCase = "y = 1";
		else
			quadrantCase = "x = 1";
	}


	// === Signal Handling Methods === //
	private void AlertSignalHandling()
	{
		if (isInstantiated == false)
		{
			alertSignal = Instantiate(AlertPrefab, CatchPosition(), Quaternion.identity) as GameObject;
			spriteRendererForAlerts = alertSignal.GetComponent<SpriteRenderer>();
			isInstantiated = true;
		}
		HandleSignalPosition();
	}



	// Catch Position
	private Vector2 CatchPosition()
	{
		// Setting up camera
		Camera cam = Camera.main;

		// Gen Variables
		Vector2 finalPos = Vector2.zero;
		float k, l = 1;
		float x1 = 0.5f, y1 = 0.5f;
		float x2 = cam.WorldToViewportPoint(transform.position).x;
		float y2 = cam.WorldToViewportPoint(transform.position).y;

		switch (quadrantCase)
		{
			case "x = 1":
				k = 0.5f / (x2 - 1);
				l = 1 / k;
				k = k * (1 / k);
				finalPos.x = 1f;
				finalPos.y = ((k * y2) + (l * y1)) / (k + l);
				break;

			case "y = 1":
				k = 0.5f / (y2 - 1);
				l = 1 / k;
				k = k * (1 / k);
				finalPos.y = 1f;
				finalPos.x = ((k * x2) + (l * x1)) / (k + l);
				break;

			case "x = 0":
				k = (-1) * l * (x1 / x2);
				l = 1 / k;
				k = k * (1 / k);
				finalPos.x = 0.0f;
				finalPos.y = ((k * y2) + (l * y1)) / (k + l);
				break;

			case "y = 0":
				k = (-1) * l * (y1 / y2);
				l = 1 / k;
				k = k * (1 / k);
				finalPos.y = 0.0f;
				finalPos.x = ((k * x2) + (l * x1)) / (k + l);
				break;
		}
		finalPos.x = Mathf.Clamp(finalPos.x, 0.04f, 0.96f);
		finalPos.y = Mathf.Clamp(finalPos.y, 0.02f, 0.97f);

		return Camera.main.ViewportToWorldPoint(finalPos);
	}



	// Handle Signal Position
	private void HandleSignalPosition()
	{
		if (alertSignal != null)
		{
			HandleFaceRotation();
			alertSignal.transform.position = CatchPosition();
			
			CheckForAlertType();
		}
	}


	// Check for alert type
	private void CheckForAlertType()
	{
		switch(this.tag)
		{
			// cases for power up and star
			case "Shield PowerUp":

			case "Speed PowerUp":

			case "Star":
				CheckForApperance();
				break;

			// cases for missiles
			case "TargetPrediction":

			case "FlyBy":

			case "HM1":
			
			case "HM2":

			case "HM3":
				if ((transform.position - alertSignal.transform.position).magnitude <= 2.0f)
				{
                    //Destroy(alertSignal.gameObject);
                    SelfDestruct();
                }
				break;
		}
	}



	// Appering and disappering image

	/*
	The check for apperance method is important as it runs only when the instantiated object is in the hierarchy
	and thus when the object is inside the camera's viewport it will disappear and will not reappera if taken.
	*/ 

	private void CheckForApperance()
	{
		Camera cam = Camera.main;

		float currentXpos = cam.WorldToViewportPoint(transform.position).x;
		float currentYpos = cam.WorldToViewportPoint(transform.position).y;


		if ((currentXpos > 0 && currentXpos < 1) && (currentYpos > 0 && currentYpos < 1))
		{
			spriteRendererForAlerts.enabled = false;
		}
		else
		{
			spriteRendererForAlerts.enabled = true;
		}
	}


	// Handle Face Rotation
	private void HandleFaceRotation()
	{
		Vector2 directionToLook = alertSignal.transform.position - transform.position;
		float angleToRotate = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg + 180;
		alertSignal.transform.rotation = Quaternion.AngleAxis(angleToRotate, transform.forward);
	}
}