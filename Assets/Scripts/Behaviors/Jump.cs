using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior
{
    public float jumpSpeed = 100.0f;
    public float jumpBoostMultiplier = 2;

    private float defaultJumpSpeed;

    void Start()
    {
        defaultJumpSpeed = jumpSpeed;
    }

    void Update()
    {
        var canJump = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);
        var boost = _inputState.GetButtonValue(inputButtons[1]);
        var right = _inputState.GetButtonValue(inputButtons[2]);
        var left = _inputState.GetButtonValue(inputButtons[3]);

        if (_collisionState.standing)
        {
            if (canJump && holdTime < 0.1f)
                OnJump();
        }

        if (Input.GetButtonUp("Jump") && _rb2d.velocity.y > 0)
        {
            Vector2 vel = _rb2d.velocity;
            vel.y /= 1.9f;
            _rb2d.velocity = vel;
        }
        jumpSpeed = defaultJumpSpeed;
        if (boost && (left || right))
            jumpSpeed *= jumpBoostMultiplier;
    }

    protected virtual void OnJump()
    {
        var vel = _rb2d.velocity;
        _rb2d.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
