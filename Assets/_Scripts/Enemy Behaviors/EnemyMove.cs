using UnityEngine;
using System.Collections;

/** Enemy move script
*/

public class EnemyMove : AbstractEnemyBehavior
{
    public float speed = 50.0f;                                                                                                                                 // For patrolling
    public float runningSpeed = 60.0f;                                                                                                                          // For running
    public bool walking;
    public bool running;
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

    void FixedUpdate()
    {
        walking = false;
        running = false;

        if (speed < defaultSpeed)
            speed += acc;
        else if (speed > defaultSpeed)
            speed = defaultSpeed;

        dir = (int)directions;
        if (dir != dirLast)
        {
            speed = 0;
            dirLast = (int)directions;
        }

        Vector2 target = new Vector2();
        if (player.gameObject != null)
        {
            target = ((player.transform.position - transform.position).normalized);
        }

        switch (enemyFSM.states)
        {
            case States.Alert:                                                                                                                                  // Alert state
                running = true;
                speed = runningSpeed;
                directions = target.x < 0 ? Directions.Left : Directions.Right;
                rb2d.velocity = new Vector2((float)directions * speed, rb2d.velocity.y);
                break;

            case States.Caution:                                                                                                                                 // Patrol state
                running = true;
                speed = runningSpeed;
                rb2d.velocity = new Vector2((float)directions * speed, rb2d.velocity.y);
                if (CheckPath())
                    Turn();
                break;

            case States.Patrol:                                                                                                                                 // Patrol state
                walking = true;
                rb2d.velocity = new Vector2((float)directions * speed, rb2d.velocity.y);
                if (CheckPath())
                    Turn();
                break;

        }
        transform.localScale = new Vector3((int)directions, 1, 1);
    }

    bool CheckPath()                                                                                                                                            // Does a raycast to check for walls or ledges
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(11.0f * (float)directions, -9f), 22.0f, enemyCollisionState.collisionLayer);      // Look for an edge
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(12.0f * (float)directions, -5.0f), 12.0f, enemyCollisionState.collisionLayer);    // Look for a wall
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(12.0f * (float)directions, 0), 12.0f, canSee);                                    // Look for another enemy
        if (hit1.collider == null || hit2.collider != null || hit3.collider != null)
        {
            return true;
        }
        return false;
    }

    void Turn()                                                                                                                                                 // Turn the player around
    {
        directions = directions == Directions.Right ? Directions.Left : Directions.Right;
    }
}