using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handler for accessing menus
/// </summary>
public static class MenuManager
{
    public static void GoToMenu(MenuName menuName)
    {
        if (menuName == MenuName.Main)
        {
            // Load Main Menu Scene
            SceneManager.LoadScene("MainMenu");
        }
        else if (menuName == MenuName.Help)
        {
            // Load Help Menu Scene
            SceneManager.LoadScene("HelpMenu");
        }
        else if (menuName == MenuName.Pause)
        {
            // Spawn pause menu prefab
            Object.Instantiate(Resources.Load("PauseMenu"));
        }
        else if (menuName == MenuName.GameOver)
        {
            // Spawn game over menu prefab
            Object.Instantiate(Resources.Load("GameOverMenu"));
        }
    }
}
