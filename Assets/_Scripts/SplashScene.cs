using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour {

	
	
    public void ChangeScene()
    {
        // Get current scene index
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // change to missile game play scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
