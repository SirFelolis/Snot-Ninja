using UnityEngine;
using System.Collections;

/** Enemy sight state script
*/


public class EnemySight : MonoBehaviour
{
    [SerializeField]
    private LastPlayerSighting lastPlayerSighting;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool playerInSight = false;
    public bool PlayerInSight
    {
        get { return playerInSight; }
        set { return; }
    }

    [SerializeField]
    private bool playerInMeleeRange = false;
    public bool PlayerInMeleeRange
    {
        get { return playerInMeleeRange; }
        set { return; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerInSight = true;
            lastPlayerSighting.SetLastPlayerPosition(player.transform.position);
//            playerInMeleeRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerInSight = false;
            playerInMeleeRange = false;
        }
    }
}
