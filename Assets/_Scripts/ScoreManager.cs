using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    // Score Texts
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] TextMeshProUGUI totalTime;
    [SerializeField] TextMeshProUGUI starCollection;
    [SerializeField] TextMeshProUGUI bonusEarned;
    [SerializeField] TextMeshProUGUI starsDuringInGame;
    [SerializeField] TextMeshProUGUI timeShowInGameOver;
    [SerializeField] TextMeshProUGUI starsShowInGameOver;

    // ScoreBoard Update
    [SerializeField] TextMeshProUGUI scoreBoardText;
    [SerializeField] TextMeshProUGUI totalStarCollection;

    // State Variables
    [SerializeField] private float totalScore = 0, missileBonus = 0, starsCollected = 0, totalTimePlayed = 0;
    
    // Variables for score multiples
    [SerializeField] private float scoreForBonus = 15f;
    [SerializeField] private float scoreForStar = 10f;

    private void Update()
    {
        // This will check the stars collected in every frame
        starsDuringInGame.text = starsCollected.ToString();

        // This will update the scoreboard every frame
        UpdateScoreBoard();
    }

    // Setter Methods for score Starts Here
    // Display and collect Star
    public void StarCollected()
    {
        starsCollected++;
        starsDuringInGame.text = starsShowInGameOver.text = starsCollected.ToString();
    }

    public void MissileBonusEarned()
    {
        missileBonus += 0.5f;
    }

    public void SetTotalTimePlayed(float timePlayed)
    {
        totalTimePlayed = timePlayed;
    }
    // Setter Methods for score Ends Here

    public void UpdateScoreBoard()
    {
        // update high score every frame
        scoreBoardText.text = PlayerPrefs.GetInt("High Score", 0).ToString();

        // update total stars collected every frame
        totalStarCollection.text = PlayerPrefs.GetInt("Stars", 0).ToString();
    }
        
    // Final Score Method
    private void CalculateFinalScore()
    {
        float missileScore = (missileBonus) * scoreForBonus;
        float starCollectedScore = starsCollected * scoreForStar;

        totalScore = missileScore + starCollectedScore + totalTimePlayed;

        // Fetch Player Stars Collection
        int starsCollection = PlayerPrefs.GetInt("Stars", 0);
        starsCollection += (int) starsCollected;

        // Insert Player Stars Collection
        PlayerPrefs.SetInt("Stars", starsCollection);



        // Fetch player score
        int scoreBoard = PlayerPrefs.GetInt("High Score", 0);

        if (totalScore > scoreBoard)
        {
            scoreBoard = (int)totalScore;
            PlayerPrefs.SetInt("High Score", scoreBoard);
        }

    }

    // Display Score on final page
    public void DisplayFinalScore()
    {
        // Calling getter method from time manager
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        SetTotalTimePlayed(timeManager.GetPlayTime());

        // Calculating final score
        CalculateFinalScore();

        // Displaying score on game over canvas
        finalScore.text = totalScore.ToString();
        starCollection.text = starsCollected.ToString();
        totalTime.text = totalTimePlayed.ToString();
        bonusEarned.text = missileBonus.ToString();

        // Displaying final time
        float minutesPlayed = totalTimePlayed / 60;
        float secondsPlayed = totalTimePlayed % 60;
        timeShowInGameOver.text = minutesPlayed.ToString("F0") + ":" + secondsPlayed.ToString("F0");
    }

    // This method will reset the score
    public void ResetScore()
    {
        totalScore = 0;
        missileBonus = 0;
        starsCollected = 0;
        totalTimePlayed = 0;
    }
}