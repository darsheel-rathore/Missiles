using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAlertSystem : MonoBehaviour {

	[Header("Missile Alert")]
	[SerializeField] GameObject missileAlertPrefab;
	string quadrantCase;
	bool isInstantited = false;
	public GameObject alertSignal;
	Transform player;

	AudioSource audioSource;
	bool isGameRunning = true;


	// Use this for initialization
	void Start () {
		if (this.tag != "Star")
		{
			audioSource = GetComponent<AudioSource>();
		}
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForPlayer();
		if (isGameRunning)
		{
			CheckIncomingDirection();
			if (this.tag != "Star")
			{
				UpdateSound();
			}
		}
		else
		{
			SelfDestruct();
		}
		AlertSignalHandling();
		
	}

	private void SelfDestruct()
	{
		Destroy(this.gameObject, 1.5f);
	}

	// Volume Control

	void UpdateSound()
	{
		float distanceFromFlight = (player.position - transform.position).magnitude;	
		if (distanceFromFlight <= 10f)
		{
			audioSource.volume = (10f - distanceFromFlight);
		}
		else
		{
			audioSource.volume = 0f;
		}
	}


	// AlertSignal Handling Methods
	
	private void AlertSignalHandling()
	{
		if (isInstantited == false)
		{
			alertSignal = Instantiate(missileAlertPrefab, CatchPosition(), Quaternion.identity) as GameObject;
			isInstantited = true;
		}
		HandleSignalPosition();
	}


	private void HandleSignalPosition()
	{
		if (alertSignal != null)
		{
			HandleFaceRotation();
			alertSignal.transform.position = CatchPosition();
			if ((transform.position - alertSignal.transform.position).magnitude <= 2.0f)
			{
				Destroy(alertSignal.gameObject);
			}
		}
		else
		{
			return;
		}
		
	}

	private void CheckIncomingDirection()
	{
		Vector3 direction = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - transform.position;

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

	private void HandleFaceRotation()
	{
		Vector2 directionToLook = alertSignal.transform.position - transform.position;
		float angleToRotate = Mathf.Atan2(directionToLook.y, directionToLook.x) * Mathf.Rad2Deg + 180;
		alertSignal.transform.rotation = Quaternion.AngleAxis(angleToRotate, transform.forward);
	}

	private void CheckForPlayer()
	{
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			isGameRunning = true;
		}
		else
		{
			isGameRunning = false;
		}
	}
}