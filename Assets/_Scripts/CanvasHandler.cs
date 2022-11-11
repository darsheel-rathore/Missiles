using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour {

    [SerializeField] GameObject startMenuCanvas;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject settingCanvas;
    [SerializeField] GameObject underDevCanvas;

    // Audio
    [SerializeField] AudioClip menuSoundClip;
    AudioSource audioSource;


    private bool disable = false;
    private bool enable = true;

    private SpawnManager spawnManagerScript;
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private BackgroundMusic backgroundMusicScript;

    void Start()
    {
        // Grab Spawn Manager Script
        spawnManagerScript = GameObject.FindGameObjectWithTag("Spawn Manager").GetComponent<SpawnManager>();

        // Grab time manager script
        timeManager = GameObject.FindGameObjectWithTag("Time Manager").GetComponent<TimeManager>();

        // Grab Score Manager Script
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();

        // Grab Background Music Script
        backgroundMusicScript = GameObject.FindGameObjectWithTag("Background Music").GetComponent<BackgroundMusic>();

        // Grabbing Audio Source
        audioSource = GetComponent<AudioSource>();

        // Game Launch Method
        LaunchGame();
    }

    private void LaunchGame()
    {
        ControlStartMenuCanvas(enable);
        ControlPauseCanvas(disable);
        ControlIngameCanvas(disable);
        ControlGameOverCanvas(disable);
        ControlSettingCanvas(disable);
        ControlUnderDevCanvas(disable);

        // Instantiate cloud and player for the very first time
        spawnManagerScript.LaunchGame();
    }
    
    
    public void PlayButton()
    {
        // Start Game
        ControlIngameCanvas(enable);

        // Disable Existing canvases
        ControlStartMenuCanvas(disable);
        ControlGameOverCanvas(disable);

        // Instantiate Clouds and Player if not present in hierarchy
        spawnManagerScript.LaunchGame();

        // Instantiate Missiles, PowerUps and Stars
        spawnManagerScript.StartGame();

        // Resetting Timer
        timeManager.StartTimer();

        // Reset Score
        scoreManager.ResetScore();

        // Play Sound
        PlayMenuAudio();
    }


    // In-Game Canvas Method
    public void PauseButton()
    {
        ControlPauseCanvas(enable);
        Time.timeScale = 0f;

        // Play Sound
        PlayMenuAudio();
    }


    // Pause Canvas Methods Starts Here
    public void ResumeGameButton()
    {
        ControlPauseCanvas(disable);
        Time.timeScale = 1f;

        // Play Sound
        PlayMenuAudio();
    }

    public void RestartButton()
    {
        ResumeGameButton();

        // Resetting game Objects by Re-Instantiating Player
        spawnManagerScript.ResetGame();

        // Resetting Timer
        timeManager.StartTimer();

        // Play Sound
        PlayMenuAudio();

    }

    // Pause Canvas Methods Ends Here


    // Pause / Gameover Canvas Common Method
    public void HomeButton()
    {
        // Resetting time.timescale
        Time.timeScale = 1f;

        // Destroy All Game Object
        spawnManagerScript.HomeKeyPressed();

        // Spanw clouds and Player
        LaunchGame();

        // Play Sound
        PlayMenuAudio();
    }

    // Game Over Canvas Methods
    public void GameOverCanvas()
    {
        ControlGameOverCanvas(enable);
        ControlIngameCanvas(disable);
    }

    // Setting Canvas Starts Here // 
    public void OpenSettingCanvas()
    {
        ControlSettingCanvas(enable);
    }

    public void CloseSettingCanvas()
    {
        ControlSettingCanvas(disable);
    }
    // Setting Canvas Ends Here // 

    // Under Development Canvas methods Starts Here
    public void LaunchUnderDevcanvas()
    {
        ControlUnderDevCanvas(enable);
    }

    // Under Dev Canvas Ends Here


    // This method will manage audio listener
    public void ManageGameAudio()
    {
        if(AudioListener.volume == 1f)
        {
            // Mute Audio
            AudioListener.volume = 0f;
        }
        else if(AudioListener.volume == 0f)
        {
            // Unmute audio
            AudioListener.volume = 1f;
        }
    }

    public void ManageBackgroundMusic()
    {
        backgroundMusicScript.ManageBackgroundMusic();
    }
    
    // This method will quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Control Methods for canvases

    private void ControlStartMenuCanvas(bool isEnabled)
    {
        startMenuCanvas.SetActive(isEnabled);
    }

    private void ControlPauseCanvas(bool isEnabled)
    {
        pauseCanvas.SetActive(isEnabled);
    }

    private void ControlIngameCanvas(bool isEnabled)
    {
        inGameCanvas.SetActive(isEnabled);
    }

    private void ControlGameOverCanvas(bool isEnabled)
    {
        gameOverCanvas.SetActive(isEnabled);
    }

    private void ControlUnderDevCanvas(bool isEnabled)
    {
        underDevCanvas.SetActive(isEnabled);
    }

    private void ControlSettingCanvas(bool isEnabled)
    {
        settingCanvas.SetActive(isEnabled);
    }

    // Menu Sound Method
    private void PlayMenuAudio()
    {
        audioSource.PlayOneShot(menuSoundClip, 0.5f);
    }

}
