using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
    public string firstLevel;
    public string savedLevel;

    [SerializeField]
    private MainMenuCamera mainMenuCamera;

    public void StartGameNew() // Starts a new game
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void StartGameContinue() // Continues saved game
    {
        SceneManager.LoadScene(savedLevel);
    }

    public void SettingsMenu() // Changes to settings menu
    {
        mainMenuCamera.TargetName = "Settings";
    }

    public void MainMenu() // Changes to main menu
    {
        mainMenuCamera.TargetName = "Main";
    }

    public void ExitGame() // Exits the game
    {
        Application.Quit();
    }
}
