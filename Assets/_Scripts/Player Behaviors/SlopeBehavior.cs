using UnityEngine;
using System.Collections;

public class SlopeBehavior : AbstractBehavior
{
    public LayerMask whatIsGround;

    public bool slidingDownSlope = false;
    public float slideSpeed = 50;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        slidingDownSlope = false;
        ToggleScripts(true);

        var moving = _inputState.GetButtonValue(inputButtons[0]) || _inputState.GetButtonValue(inputButtons[1]);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 15f, whatIsGround); //cast downwards
        
        if (hit.collider != null && !moving && _collisionState.standing)
        {
            float angle = Mathf.Atan2(hit.normal.x, hit.normal.y) * Mathf.Rad2Deg; // Get angle

            if (Mathf.Abs(angle) > 15)
            {
                ToggleScripts(false);
                _rb2d.velocity += new Vector2(0, -slideSpeed);
                slidingDownSlope = true;
            }

            if (angle > 15)
            {
                _rb2d.velocity += new Vector2(slideSpeed, 0);
            }
            if (angle < -15)
            {
                _rb2d.velocity += new Vector2(-slideSpeed, 0);
            }

        }
	}
}
