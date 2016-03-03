using UnityEngine;
using System.Collections;

public class CrouchBehavior : AbstractBehavior
{

    public bool crouching;

	void Update ()
    {
        var crouch = _inputState.GetButtonValue(inputButtons[0]);
        if (crouch && _collisionState.standing)
        {
            _rb2d.velocity = Vector2.zero;
            crouching = true;
            ToggleScripts(false);
        }
        else
        {
            crouching = false;
            ToggleScripts(true);
        }
    }
}
