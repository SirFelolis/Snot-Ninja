using UnityEngine;
using System.Collections;

public class WallClimbBehavior : AbstractBehavior
{
    public float climbSpeed;
    public float climbingTime;
    public bool climbing = false;

    private bool hasClimbed = false;
    public bool canClimb = true;

    private float counter;

    void FixedUpdate()
    {
        if (Mathf.Abs(_rb2d.velocity.x) >= 110)
            canClimb = true;
        else if (Mathf.Abs(_rb2d.velocity.x) < 110 && !climbing)
            canClimb = false;

        climbing = false;

        var left = _inputState.GetButtonValue(inputButtons[0]);
        var right = _inputState.GetButtonValue(inputButtons[1]);

        if (_collisionState.standing)
            hasClimbed = false;

        if (counter >= climbingTime)
            hasClimbed = true;

        if (left || right)
        {
            if (!_collisionState.standing && _collisionState.onWall && counter < climbingTime && !hasClimbed && canClimb)
            {
                counter++;
                climbing = true;
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0);
                _rb2d.velocity += new Vector2(0, climbSpeed);
            }
        }
        else
            counter = 0;
    }
}
