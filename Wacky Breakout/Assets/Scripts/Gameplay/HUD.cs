using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Score Text Support
    [SerializeField]
    Text scoreText;
    int score;
    string scorePrefix = "Score: ";

    // Balls Left Text Support
    [SerializeField]
    Text ballsLeftText;
    int ballsLeft;
    string ballsLeftPrefix = "Balls Left: ";

    // Properties

    /// <summary>
    /// Returns the game score
    /// </summary>
    public int Score
    {
        get { return score; }
    }

    /// <summary>
    /// Returns the no of balls left
    /// </summary>
    public int BallsLeft
    {
        get { return ballsLeft; }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Initialise balls left from config utils
        ballsLeft = ConfigurationUtils.NoOfBalls;
        ballsLeftText.text = ballsLeftPrefix + ballsLeft;

        // Initialiasing the score text
        scoreText.text = scorePrefix + score;

        // Add listeners for events
        EventManager.AddPointsAddedListener(AddPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds points to score
    /// </summary>
    /// <param name="points"> points </param>
    public void AddPoints(int points)
    {
        if(points > 0)
        {
            // Update score
            score += points;

            // Update text
            scoreText.text = scorePrefix + score;
        }
    }

    /// <summary>
    /// Updates number of balls left and reduces by -1
    /// </summary>
    public void UpdateBallsLeft()
    {
        if(ballsLeft > 0) 
        {
            // Update ball tally
            ballsLeft -= 1;

            // Update text
            ballsLeftText.text = ballsLeftPrefix + ballsLeft;
        }
    }
}
