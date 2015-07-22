﻿using UnityEngine;

public class Walk : AbstractBehavior
{
    public float speed = 50.0f;
    public float runMultiplier = 1.0f;
    public bool running;
    public bool moving;

	void Update ()
    {
        running = false;
        moving = false;

        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);
        var run = _inputState.GetButtonValue(inputButtons[2]);

        if (right || left)
        {
            var currentSpeed = speed;

            moving = true;

            if (run && runMultiplier > 0)
            {
                currentSpeed *= runMultiplier;
                running = true;
            }

            var velX = currentSpeed * Input.GetAxis("Horizontal");

            _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
        }
	}
}
