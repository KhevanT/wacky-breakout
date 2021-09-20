using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pause menu that stops gameplay and loads a 
/// menu with resume and quit buttons
/// </summary>
public class PauseMenu : MonoBehaviour
{
    // Fields
    Timer resumeTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Pause the game when added
        Time.timeScale = 0;

        // Add timer component
        resumeTimer = gameObject.AddComponent<Timer>();
        resumeTimer.Duration = 0.1f;
    }

    /// <summary>
    /// Handles the OnClick() event for the resume button
    /// </summary>
    public void HandleResumeButtonOnClickEvent()
    {
        // Play sound effect
        AudioManager.Play(AudioClipName.ButtonClick);

        // Destroy pause menu object
        Destroy(gameObject);

        // Unpause the game
        Time.timeScale = 1;

        /*
        // Run resume timer
        resumeTimer.Run();
        Debug.Log("Resume timer has started");

        // Wait until resume timer is finished
        // [BUGGY]
        if (resumeTimer.Finished)
        {
            // Stop timer
            resumeTimer.Stop();
            Debug.Log("Resume timer has ended");

            // Unpause the game
            Time.timeScale = 1;
        }
        */
    }

    /// <summary>
    /// Handles the OnClick() event for the quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        // Play sound effect
        AudioManager.Play(AudioClipName.ButtonClick);

        // Unpause the game
        Time.timeScale = 1;

        // Destroy pause menu object
        Destroy(gameObject);

        // Go to main menu
        MenuManager.GoToMenu(MenuName.Main);
    }
}
