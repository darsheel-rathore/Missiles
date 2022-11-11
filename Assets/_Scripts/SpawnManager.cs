using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject missileSpawnerPrefab;
    [SerializeField] GameObject cloudSpawnerPrefab;
    [SerializeField] GameObject starSpawnerPrefab;
    [SerializeField] GameObject powerUpSpawnerPrefab;

    ValidatePlayer validatePlayer;
    ValidatePlayer checkPlayer;

    private Player playerScript;
    private Vector3 playerPosition;

    private void Start()
    {
        checkPlayer = GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>();
    }

    private void Update()
    {
        GrabPlayerPosition();
    }

    private void GrabPlayerPosition()
    {
        if (CheckForPlayer())
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerPosition = playerScript.GetAircraftPosition();
        }
    }

    public void LaunchGame()
    {
        // Check if cloud spawner exists in the scene
        if (GameObject.FindGameObjectWithTag("Cloud Spawner") == null)
        {
            // Instantiate Cloud Spawner Prefab
            InstantiatePrefab(cloudSpawnerPrefab);
        }

        // Check if the player exists in the scene
        if (!CheckForPlayer())
        {
            // Instantiate Player Prefab
            InstantiatePrefab(playerPrefab);
        }
      
    }

    public void StartGame()
    {
        // Instantiate Missile Spawner Prefab
        InstantiatePrefab(missileSpawnerPrefab);


        // Instantiate Star Spawner Prefab
        InstantiatePrefab(starSpawnerPrefab);


        // Instantiate Power-Up Spawner Prefab
        InstantiatePrefab(powerUpSpawnerPrefab);
    }


    public void ResetGame()
    {
        // Destroy all gameobject
        DestroyAllGameObject();

        // Reset Rotation of the plane
        //PlayerResetMethodCall();

        // Start Game
        StartGame();
    }


    public void HomeKeyPressed()
    {
        // Destroy all gameObject
        DestroyAllGameObject();

        // Check if player exists or not
        if (GameObject.FindGameObjectWithTag("Validate").GetComponent<ValidatePlayer>().isPlayerAlive())
        {
            // Reset player Rotation
            //PlayerResetMethodCall();
        }
        else
        {
            // Instantiate Player at its last known position
            Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        }
    }

    // This Method helps to Instantiate Game Object
    private void InstantiatePrefab(GameObject prefab)
    {
        Instantiate(
            prefab,
            transform.position,
            Quaternion.identity);
    }

    // This method will kill all the game object
    private void DestroyAllGameObject()
    {
        foreach (GameObject gameObject in Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (gameObject.layer == 8)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool CheckForPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void PlayerResetMethodCall()
    {
        if (CheckForPlayer())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ResetPositionPlayer();
        }
    }
    
}
