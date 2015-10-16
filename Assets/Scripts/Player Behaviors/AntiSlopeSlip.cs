using UnityEngine;
using System.Collections;

public class AntiSlopeSlip : AbstractBehavior
{
	void FixedUpdate ()
	{
        if (_collisionState.standing)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10f, _collisionState.collisionLayer);

            if (hit)
            {
                _rb2d.velocity -= new Vector2(hit.normal.x * 6.385f, 0);

                var pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(_rb2d.velocity.x) * Time.deltaTime * (_rb2d.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = pos;
            }
        }
	}
}
