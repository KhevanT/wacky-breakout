using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game over screen that displays score and quit to main menu option
/// </summary>
public class GameOverMenu : MonoBehaviour
{
    // Fields

    // Score Text Support
    [SerializeField]
    Text scoreText;
    string scorePrefix = "Your score was : ";

    // Start is called before the first frame update
    void Start()
    {
        // Stop game
        Time.timeScale = 0;

        // Initialising hud
        HUD hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        // Displaying the score text
        scoreText.text = scorePrefix + hud.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Handles the OnClick() event for the quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        // Play sound effect
        AudioManager.Play(AudioClipName.ButtonClick);

        // Destroy pause menu object
        Destroy(gameObject);

        // Go to main menu
        MenuManager.GoToMenu(MenuName.Main);
    }
}
