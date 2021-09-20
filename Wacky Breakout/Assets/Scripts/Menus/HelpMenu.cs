using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks for Escape key being pressed down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Goes to main menu
            MenuManager.GoToMenu(MenuName.Main);
        }
    }
}
