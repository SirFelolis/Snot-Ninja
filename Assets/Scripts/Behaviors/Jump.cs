using UnityEngine;

public class Jump : AbstractBehavior
{
    // Dear lord save me from these comments
    // I'll get rid of them some day
    // Some day...

    public float jumpSpeed = 160.0f;
    public float jumpDelay = .1f;
    public int jumpCount = 2;

    protected float lastJumpTime = 0;
    protected int jumpsRemaining = 0;
/*    public float jumpBoostMultiplier = 1.2f;
    public float WallJumpBoostMultiplier = 2;
    public float resetDelay = .2f;
    public bool jumpingOffWall;
    public Vector2 wallJumpSpeed = new Vector2(50, 150);*/

    private float defaultJumpSpeed;
//    private Vector2 defaultWallJumpSpeed;

    void Start()
    {
        defaultJumpSpeed = jumpSpeed;
//        defaultWallJumpSpeed = wallJumpSpeed;
    }

    void Update()
    {
        var canJump = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);
        //        var boost = _inputState.GetButtonValue(inputButtons[1]);
        //        var right = _inputState.GetButtonValue(inputButtons[2]);
        //        var left = _inputState.GetButtonValue(inputButtons[3]);
        //        var onWall = _collisionState.onWall;

        if (_collisionState.standing)
        {
            jumpsRemaining = jumpCount - 1;
            if (canJump && holdTime < 0.1f)
            {
                OnJump();
            }
        }
        else
        {
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay)
            {
                if (jumpsRemaining > 0)
                {
                    OnJump();
                    jumpsRemaining--;
                }
            }
        }

        /*        else if (!_collisionState.standing && onWall)
                {
                    if ((canJump && holdTime < 0.1f) && !jumpingOffWall) // (Oliver)Wall jumping is temporarily disabled because I'm not sure if I want it in the game or not.
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
                }*/

        if (Input.GetButtonUp("Jump") && _rb2d.velocity.y > 0)
        {
            Vector2 vel = _rb2d.velocity;
            vel.y /= 1.9f;
            _rb2d.velocity = vel;
        }
        jumpSpeed = defaultJumpSpeed;
  //      wallJumpSpeed = defaultWallJumpSpeed;
/*        if (boost)
        {
            wallJumpSpeed.x *= WallJumpBoostMultiplier;
            if (left || right)
            {
                jumpSpeed *= jumpBoostMultiplier;
            }
        }*/
    }

    protected virtual void OnJump()
    {
        var vel = _rb2d.velocity;
        lastJumpTime = Time.time;
        _rb2d.velocity = new Vector2(vel.x, jumpSpeed);
    }

    /*    protected virtual void OnWallJump()
        {
            _inputState.direction = _inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
            _rb2d.velocity = new Vector2(wallJumpSpeed.x * (float)_inputState.direction, wallJumpSpeed.y);
        }*/
}
