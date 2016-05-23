using UnityEngine;
using System.Collections;

namespace Player
{
    public class CrouchBehavior : AbstractBehavior
    {
        public bool crouching;

        void Update()
        {
            var crouch = _inputState.GetButtonValue(inputButtons[0]);
            if (crouch && _collisionState.standing && _inputState.absVelX < 5)
            {
                _rb2d.velocity = Vector2.zero;
                crouching = true;
                ToggleScripts(false);
            }
            else if (crouching)
            {
                crouching = false;
                ToggleScripts(true);
            }
        }
    }
}