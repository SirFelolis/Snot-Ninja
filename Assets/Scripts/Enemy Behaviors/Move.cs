using UnityEngine;
using System.Collections;

public enum EnemyBehaviors
{
    Follow,
    Patrol
};

public class Move : AbstractEnemyBehavior
{
    public Transform player;
    public float speed = 50.0f;
    public bool moving;
    public EnemyBehaviors behavior;
    public LayerMask layerMask;

    void Start()
    {
        directions = Random.Range(0, 9) >= 4 ? Directions.Left : Directions.Right;
    }

    void Update()
    {
        moving = false;

        switch(behavior)
        {
            case EnemyBehaviors.Follow:
                moving = true;
                var target = ((player.position - transform.position).normalized);
                _rb2d.velocity = new Vector2(Mathf.Sign(target.x) * speed, _rb2d.velocity.y);
                break;
            case EnemyBehaviors.Patrol:
                moving = true;
                RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, new Vector2(32 * (float)directions, -16), 32, layerMask);
                RaycastHit2D hitWall = Physics2D.Raycast(transform.position, new Vector2(16 * (float)directions, 0), 16, layerMask);
                _rb2d.velocity = new Vector2((float)directions * speed, _rb2d.velocity.y);
                if (hitFloor.collider == null)
                    Turn();
                if (hitWall.collider != null)
                    Turn();
                break;
        }
    }

    void Turn()
    {
        directions = directions == Directions.Right ? Directions.Left : Directions.Right;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector2(32 * (float)directions, -32));
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, new Vector2(16 * (float)directions, 0));
    }
}