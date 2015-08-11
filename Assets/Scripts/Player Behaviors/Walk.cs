using UnityEngine;

/** Player walk script
*/

public class Walk : AbstractBehavior
{
    public float speed = 50.0f;
    public float runMultiplier = 1.0f;
    public bool moving;

	void Update ()
    {
        moving = false;

        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);

        if (right || left)
        {
            var currentSpeed = speed;

            moving = true;

            var velX = currentSpeed * Input.GetAxis("Horizontal");

            _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
        }
    }
}
