using UnityEngine;
using System.Collections;

/** Enemy abstract behavior script
*/


public abstract class AbstractEnemyBehavior : MonoBehaviour
{
    public MonoBehaviour[] disableScripts;

    protected GameObject player;
    protected Rigidbody2D rb2d;
    protected EnemyCollisionState enemyCollisionState;
    protected LastPlayerSighting lastPlayerSighting;
    protected EnemyFSM enemyFSM;
    protected Directions directions;

    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        enemyCollisionState = GetComponent<EnemyCollisionState>();
        lastPlayerSighting = GetComponent<LastPlayerSighting>();
        enemyFSM = GetComponent<EnemyFSM>();
    }

    protected virtual void ToggleScripts(bool value)
    {
        foreach (var script in disableScripts)
        {
            script.enabled = value;
        }
    }

}
