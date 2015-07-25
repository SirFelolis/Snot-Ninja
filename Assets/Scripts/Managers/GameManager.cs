using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player; // This is the game manager of course we need to track the player DUUUUH
    public Transform playerRespawn; // This is the game manager of course we need to track the player respawn position DUUUUH
    public bool playerDead;

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

        if (GameObject.Find(player.name) == null)
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