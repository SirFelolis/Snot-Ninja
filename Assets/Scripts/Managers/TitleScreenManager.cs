using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
    public string firstLevel;
    public string savedLevel;
    public string optionsMenu;
    public string mainMenu;
    public string lastMenu;

    public void StartGameNew() // Starts a new game
    {
        Application.LoadLevel(firstLevel);
    }

    public void StartGameContinue() // Continues saved game
    {
        Application.LoadLevel(savedLevel);
    }

    public void ExitGame() // Exits the game
    {
        Application.Quit();
    }

    public void Options() // Go to the options menu
    {
        Application.LoadLevel(optionsMenu);
    }

    public void Return() // Returns back to the main menu (probably from the options)
    {
        Application.LoadLevel(lastMenu);
    }
}
