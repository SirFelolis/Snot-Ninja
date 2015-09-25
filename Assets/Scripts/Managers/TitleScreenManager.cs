using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
    public void StartGameNew() // Starts a new game
    {
        Application.LoadLevel(2);
    }

    public void StartGameContinue() // Continues saved game
    {
    }

    public void ExitGame() // Exits the game
    {
        Application.Quit();
    }

    public void Options() // Go to the options menu
    {
        Application.LoadLevel(1);
    }

    public void Return() // Returns back to the main menu (probably from the options)
    {
        Application.LoadLevel(0);
    }
}
