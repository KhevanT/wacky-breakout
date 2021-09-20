using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main script for the game
/// Also acts as a hub for some miscellaneous functions
/// </summary>
public class WackyBreakout : MonoBehaviour
{
    // Fields

    // Game over event support
    HUD hud;

    // Start is called before the first frame update
    void Start()
    {
        // Get hud
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks for Escape key being pressed down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Goes to pause menu
            MenuManager.GoToMenu(MenuName.Pause);
        }

        // Checks if there are any balls left or blocks left
        if (hud.BallsLeft <= 0 && !GameObject.FindGameObjectWithTag("Ball") ||
            !GameObject.FindGameObjectWithTag("Block"))
        {
            HandleGameOverEvent();
        }
    }

    /// <summary>
    /// Handles game over event
    /// </summary>
    void HandleGameOverEvent()
    {
        if (!GameObject.FindGameObjectWithTag("Game Over Menu"))
        {
            // Goes to game over menu
            MenuManager.GoToMenu(MenuName.GameOver);
        }
    }
}
