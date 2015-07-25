using UnityEngine;
using System.Collections;

public enum EnemyBehaviors
{
    Follow
};

public class Move : AbstractEnemyBehavior
{
    public Transform player;
    public float speed = 50.0f;
    public bool moving;
    public EnemyBehaviors behavior;

    void Update()
    {
        moving = false;

        switch(behavior)
        {
            case EnemyBehaviors.Follow:
                var target = ((player.position - transform.position).normalized * speed);

                _rb2d.velocity = new Vector2(target.x, _rb2d.velocity.y);
                moving = true;
                break;
        }

        Debug.Log((player.position - transform.position).normalized * speed);
    }
}
