using UnityEngine;
using UnityEngine.SceneManagement;

/** Game manager script
*/


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool playerDead;

    private string _playerName;

    void Awake()
    {
        _playerName = player.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
//            Application.Quit();
        }

        if (GameObject.Find(_playerName) == null)
        {
            if (!playerDead)
            {
                playerDead = true;
            }
        }

        if (playerDead)
        {
            Respawn();
        }
    }

    void Respawn(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}