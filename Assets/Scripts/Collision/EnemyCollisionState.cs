using UnityEngine;
using System.Collections;

/** Enemy collision state script
*/

public class EnemyCollisionState : MonoBehaviour
{
    public LayerMask collisionLayer;
    public bool standing;
    public bool onWall;
    public bool onLeftEdge;
    public bool onRightEdge;
    public Vector2 bottomPosition = Vector2.zero;
    public float collisionRadius = 10.0f;
    public Color debugCollisionColor = Color.red;

    void FixedUpdate()
    {
        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] {bottomPosition};

        foreach (var position in positions)
        {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            Gizmos.DrawWireSphere(pos, collisionRadius);
        }
    }
}
