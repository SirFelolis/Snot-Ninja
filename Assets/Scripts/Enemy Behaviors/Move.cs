using UnityEngine;
using System.Collections;

/** Enemy move script
*/

public class Move : AbstractEnemyBehavior
{
    public float speed = 50.0f; // For patrolling
    public float speedMultiplier = 1.1f; // When running
    public bool moving;
    public float acc = 1.0f;

    private float defaultSpeed;

    private float dir = 0.0f;
    private float dirLast = 0.0f;


    void Start()
    {
        defaultSpeed = speed;

        directions = Random.Range(0, 9) >= 4 ? Directions.Left : Directions.Right;
    }

    void Update()
    {
        moving = false;
        if (speed < defaultSpeed)
            speed += acc;
        else if (speed > defaultSpeed && _enemyFSM.behavior != EnemyBehaviors.Follow)
            speed = defaultSpeed;

        dir = (float)directions;
        if (dir != dirLast)
        {
            speed = 0;
            dirLast = (float)directions;
        }

        Vector2 target = Vector2.zero;

        if (_player.gameObject != null)
        {
            target = ((_player.transform.position - transform.position).normalized);
        }


        switch (_enemyFSM.behavior)
        {
            case EnemyBehaviors.Follow:
                moving = true;
                if (speed < defaultSpeed * speedMultiplier)
                    speed *= speedMultiplier;

                directions = target.x < 0 ? Directions.Left : Directions.Right;
                _rb2d.velocity = new Vector2(speed * (float)directions, _rb2d.velocity.y);
                break;
            case EnemyBehaviors.Patrol:
                moving = true;
                _rb2d.velocity = new Vector2((float)directions * speed, _rb2d.velocity.y);
                RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(22.0f * (float)directions, -22.0f), 22.0f, _enemyCollisionState.collisionLayer);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(10.0f * (float)directions, 0.0f), 10.0f, _enemyCollisionState.collisionLayer);
                if (hit1.collider == null || hit2.collider != null)
                {
                    Turn();
                }
                break;
        }
    }

    void Turn()
    {
        directions = directions == Directions.Right ? Directions.Left : Directions.Right;
    }

    void OnDrawGizmos()
    {
/*        if(_enemySightSate.behavior == EnemyBehaviors.Patrol)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, new Vector2(22 * (float)directions, -22)); // For some reason generates a null refrence exception
            Gizmos.DrawRay(transform.position, new Vector2(10 * (float)directions, 0.0f));
        }*/
    }
}