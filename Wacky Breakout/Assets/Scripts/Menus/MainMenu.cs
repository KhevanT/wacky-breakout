using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu handler
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Make methods for the onclick events fdor each button

    /// <summary>
    /// Handles OnClick() event for Play Button
    /// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        // Play sound effect
        AudioManager.Play(AudioClipName.ButtonClick);

        // Load GamePlay scene
        SceneManager.LoadScene("GamePlay");
    }

    /// <summary>
    /// Handles OnClick() event for Help Button
    /// </summary>
    public void HandleHelpButtonOnClickEvent()
    {
        // Play sound effect
        AudioManager.Play(AudioClipName.ButtonClick);

        MenuManager.GoToMenu(MenuName.Help);
    }

    /// <summary>
    /// Handles OnClick() event for Quit Button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        // Play sound effect
        AudioManager.Play(AudioClipName.ButtonClick);

        // Quit game
        Application.Quit();
    }
}
