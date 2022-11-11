using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {

	[SerializeField] GameObject[] Missile;

    private ValidatePlayer checkPlayer;

    float xValue, yValue;
    int countHM1 = 4, countHM2 = 5, countHM3 = 14, countHM4 = 16, countTP = 11, countFB = 12;

	// Use this for initialization
	void Start () {
        checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
        MissileLaunch();
    }

    void Update()
    {
        CheckForPlayer();
    }

    // This method will check player and launch coroutine
    private void MissileLaunch()
    {
        if (checkPlayer.isPlayerAlive())
        {
            InitiateMissileLaunch();
        }
    }

    // This method will check for player and Destroy game object
    private void CheckForPlayer()
    {
        if (checkPlayer.isPlayerAlive() == false)
        {
            Destroy(this.gameObject);

        }
    }

    void InitiateMissileLaunch()
	{
		StartCoroutine(SpawnHM1());
        StartCoroutine(SpawnHM2());
        StartCoroutine(SpawnHM3());
        StartCoroutine(SpawnHM4());
        StartCoroutine(SpawnTP());
        StartCoroutine(SpawnFB());
    }


	IEnumerator SpawnHM1()
	{
		for (int i = 0; i < countHM1; i++)
		{
			CalculateBoundry();
			Instantiate(Missile[0], new Vector3(xValue, yValue, 0), Quaternion.identity);
			yield return new WaitForSeconds(CalculateRandomTime(7f, 9f));
		}
	}

	IEnumerator SpawnHM2()
	{
		for (int i = 0; i < countHM2; i++)
		{
			CalculateBoundry();
			yield return new WaitForSeconds(CalculateRandomTime(7f, 9f));
			Instantiate(Missile[1], new Vector3(xValue, yValue, 0), Quaternion.identity);
		}
	}

	IEnumerator SpawnHM3()
	{
		for (int i = 0; i < countHM3; i++)
		{
			CalculateBoundry();
			yield return new WaitForSeconds(CalculateRandomTime(12f, 14f));
			Instantiate(Missile[2], new Vector3(xValue, yValue, 0), Quaternion.identity);
		}
	}

	IEnumerator SpawnHM4()
	{
		for (int i = 0; i < countHM4; i++)
		{
			CalculateBoundry();
			yield return new WaitForSeconds(CalculateRandomTime(17f, 21f));
			//yield return new WaitForSeconds(CalculateRandomTime(2f, 3f));
			Instantiate(Missile[3], new Vector3(xValue, yValue, 0), Quaternion.identity);
		}
	}

	IEnumerator SpawnTP()
	{
		for (int i = 0; i < countTP; i++)
		{
			CalculateBoundry();
			yield return new WaitForSeconds(CalculateRandomTime(25f, 30f));
			Instantiate(Missile[4], new Vector3(xValue, yValue, 0), Quaternion.identity);
		}
	}

	IEnumerator SpawnFB()
	{
		for (int i = 0; i < countFB; i++)
		{
			CalculateBoundry();
			yield return new WaitForSeconds(CalculateRandomTime(15f, 20f));
			Instantiate(Missile[5], new Vector3(xValue, yValue, 0), Quaternion.identity);
		}
	}

	private void CalculateBoundry()
	{
		Vector2 finalValues =  gameObject.GetComponent<Boundries>().SetUpBoundry();
		xValue = finalValues.x;
		yValue = finalValues.y;
	}

	private float CalculateRandomTime(float minValue, float maxValue)
	{
		return Random.Range(minValue, maxValue);
	}

}
