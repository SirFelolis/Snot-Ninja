using UnityEngine;
using System.Collections;

/** Enemy collision state script
*/

public class EnemyCollisionState : MonoBehaviour
{
    public LayerMask collisionLayer;
    public bool standing;
    public bool onWall;
    public bool onLeftWall;
    public bool onRightWall;
    public Vector2 bottomPosition = Vector2.zero;
    public Vector2 rightPosition = Vector2.zero;
    public Vector2 leftPosition = Vector2.zero;
    public float collisionRadius = 10.0f;
    public Color debugCollisionColor = Color.red;

    void FixedUpdate()
    {
        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

        pos = rightPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        onRightWall = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

        pos = leftPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        onLeftWall = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

        if (onLeftWall || onRightWall)
        {
            onWall = true;
        }
        else if ((!onLeftWall || !onRightWall) && onWall)
        {
            onWall = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] { bottomPosition, rightPosition, leftPosition };

        foreach (var position in positions)
        {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            Gizmos.DrawWireSphere(pos, collisionRadius);
        }
    }
}
