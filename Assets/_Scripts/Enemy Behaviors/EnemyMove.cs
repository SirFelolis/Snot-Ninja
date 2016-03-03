using UnityEngine;
using System.Collections;

/** Enemy move script
*/

public class EnemyMove : AbstractEnemyBehavior
{
    public float speed = 50.0f; // For patrolling
    public float speedMultiplier = 1.1f; // For running
    public bool moving;
    public float acc = 1.0f;

    public LayerMask canSee;

    private float defaultSpeed;

    private float dir = 0.0f;
    private float dirLast = 0.0f;


    protected override void Awake()
    {
        base.Awake();

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

        dir = (int)directions;
        if (dir != dirLast)
        {
            speed = 0;
            dirLast = (int)directions;
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
                RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(11.0f * (float)directions, -9f), 22.0f, _enemyCollisionState.collisionLayer); // Look for an edge
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(12.0f * (float)directions, -5.0f), 12.0f, _enemyCollisionState.collisionLayer); // Look for a wall
                RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(12.0f * (float)directions, 0), 12.0f, canSee); // Look for another enemy
                if (hit1.collider == null || hit2.collider != null || hit3.collider != null)
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
}