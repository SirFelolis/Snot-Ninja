using UnityEngine;

/** Game manager script
*/


public class GameManager : MonoBehaviour
{
    public GameObject player; // This is the game manager of course we need to track the player DUUUUH
    public bool playerDead;

    private string _playerTag;

    void Awake()
    {
        _playerTag = player.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (GameObject.Find(_playerTag) == null)
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
        Application.LoadLevel(Application.loadedLevel);
    }
}