using UnityEngine;
using System.Collections;

public enum EnemyBehaviors
{
    Follow,
    Patrol
};

public class Move : AbstractEnemyBehavior
{
    public float speed = 50.0f;
    public float sightRadius;
    public bool moving;
    public bool vision;
    public EnemyBehaviors behavior;
    public Transform player;
    public LayerMask layerMask;
    public LayerMask canSee;

    void Start()
    {
        directions = Random.Range(0, 9) >= 4 ? Directions.Left : Directions.Right;
    }

    void Update()
    {

        moving = false;

        vision = Physics2D.OverlapCircle(transform.position, sightRadius, canSee);

        if (vision)
            behavior = EnemyBehaviors.Follow;
        else
            behavior = EnemyBehaviors.Patrol;

        switch (behavior)
        {
            case EnemyBehaviors.Follow:
                moving = true;
                speed = 70.0f;
                var target = ((player.position - transform.position).normalized);
                _rb2d.velocity = new Vector2(Mathf.Sign(target.x) * speed, _rb2d.velocity.y);
                break;
            case EnemyBehaviors.Patrol:
                moving = true;
                speed = 50.0f;
                _rb2d.velocity = new Vector2((float)directions * speed, _rb2d.velocity.y); //TODO: FIX THIS. COMPLETELY BROKEN. HELP
                if (!_enemyCollisionState.onLeftEdge || !_enemyCollisionState.onRightEdge)
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
        if (vision) Gizmos.color = Color.red;
        else
            Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, new Vector2(32 * (float)directions, -32));
        Gizmos.DrawRay(transform.position, new Vector2(16 * (float)directions, 0));
    }
}