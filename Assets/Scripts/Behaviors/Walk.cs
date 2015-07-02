using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior
{
    public float speed = 50.0f;
    public float runMultiplier = 2.0f;
    public bool running;
    public bool moving;

    private float currentSpeed = 0;

	void Update ()
    {
        running = false;
        moving = false;

        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);
        var run = _inputState.GetButtonValue(inputButtons[2]);

        Debug.Log(speed * Input.GetAxisRaw("Horizontal"));
        if (right || left)
        {
            currentSpeed = speed;

            moving = true;

            if (run && runMultiplier > 0)
            {
                currentSpeed *= runMultiplier;
                running = true;
            }

            var velX = currentSpeed * (float)_inputState.direction;

            _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
        }
	
	}
}
