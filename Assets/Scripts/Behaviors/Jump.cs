using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior
{
    public float jumpSpeed = 160.0f;
    public float jumpBoostMultiplier = 1.2f;
    public float WallJumpBoostMultiplier = 2;
    public float resetDelay = .2f;
    public bool jumpingOffWall;
    public Vector2 wallJumpSpeed = new Vector2(50, 150);

    private float timeElapsed = 0;
    private float defaultJumpSpeed;
    private Vector2 defaultWallJumpSpeed;

    void Start()
    {
        defaultJumpSpeed = jumpSpeed;
        defaultWallJumpSpeed = wallJumpSpeed;
    }

    void Update()
    {
        var canJump = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);
        var boost = _inputState.GetButtonValue(inputButtons[1]);
        var right = _inputState.GetButtonValue(inputButtons[2]);
        var left = _inputState.GetButtonValue(inputButtons[3]);
        var onWall = _collisionState.onWall;

        if (_collisionState.standing)
        {
            if (canJump && holdTime < 0.1f)
                OnNormalJump();
        }
        else if (!_collisionState.standing && onWall)
        {
            if ((canJump && holdTime < 0.1f) && !jumpingOffWall)
            {
                OnWallJump();

                ToggleScripts(false);
                jumpingOffWall = true;
            }
        }

        if (jumpingOffWall)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > resetDelay)
            {
                ToggleScripts(true);
                jumpingOffWall = false;
                timeElapsed = 0;
            }
        }

        if (Input.GetButtonUp("Jump") && _rb2d.velocity.y > 0)
        {
            Vector2 vel = _rb2d.velocity;
            vel.y /= 1.9f;
            _rb2d.velocity = vel;
        }
        jumpSpeed = defaultJumpSpeed;
        wallJumpSpeed = defaultWallJumpSpeed;
        if (boost)
        {
            wallJumpSpeed.x *= WallJumpBoostMultiplier;
            if (left || right)
            {
                jumpSpeed *= jumpBoostMultiplier;
            }
        }
    }

    protected virtual void OnNormalJump()
    {
        var vel = _rb2d.velocity;
        _rb2d.velocity = new Vector2(vel.x, jumpSpeed);
    }

    protected virtual void OnWallJump()
    {
        _inputState.direction = _inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
        _rb2d.velocity = new Vector2(wallJumpSpeed.x * (float)_inputState.direction, wallJumpSpeed.y);
    }
}
