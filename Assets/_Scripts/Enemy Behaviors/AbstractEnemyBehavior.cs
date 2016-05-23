using UnityEngine;
using System.Collections;

/** Enemy abstract behavior script
*/

namespace Enemy
{
    public enum Directions
    {
        Right = 1,
        Left = -1,
        None = 0
    }

    public abstract class AbstractEnemyBehavior : MonoBehaviour
    {
        public MonoBehaviour[] disableScripts;

        protected GameObject player;
        protected Rigidbody2D rb2d;
        protected EnemyCollisionState enemyCollisionState;
        protected LastPlayerSighting lastPlayerSighting;
        protected EnemyFSM enemyFSM;
        protected EnemySight enemySight;

        protected virtual void Awake()
        {
            player = GameObject.FindWithTag("Player");
            rb2d = GetComponent<Rigidbody2D>();
            enemyCollisionState = GetComponent<EnemyCollisionState>();
            lastPlayerSighting = GetComponent<LastPlayerSighting>();
            enemyFSM = GetComponent<EnemyFSM>();
            enemySight = GetComponentInChildren<EnemySight>();
        }

        protected virtual void ToggleScripts(bool value)
        {
            foreach (var script in disableScripts)
            {
                script.enabled = value;
            }
        }

    }
}
