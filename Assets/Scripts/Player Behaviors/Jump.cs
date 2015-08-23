using UnityEngine;

/** Player jump script
 * Dear lord save me from these comments
 * I'll get rid of them some day
 * Some day...
*/


public class Jump : AbstractBehavior
{
    public float jumpSpeed = 160.0f;
    public float jumpDelay = .2f;
    public int jumpCount = 2;

    protected float lastJumpTime = 0;
    protected int jumpsRemaining = 0;

    private float timesJumped;
    private bool canJump;
    private bool jumpReleased;
    private float holdTime;

    void Update()
    {
        jumpReleased = Input.GetButtonUp("Jump");
        canJump = _inputState.GetButtonValue(inputButtons[0]);
        holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (jumpReleased && _rb2d.velocity.y > 0)
        {
            Vector2 vel = _rb2d.velocity;
            vel.y /= 1.9f;
            _rb2d.velocity = vel;
        }

        Debug.Log(timesJumped);

        if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay)
            timesJumped++;

        if (_collisionState.standing)
        {
            jumpsRemaining = jumpCount;
            timesJumped = 0;
            if (canJump && holdTime < .1f)
            {
                OnJump();
                jumpsRemaining--;
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
    }

    protected virtual void OnJump()
    {
        var vel = _rb2d.velocity;
        lastJumpTime = Time.time;
        _rb2d.velocity = new Vector2(vel.x, jumpSpeed);
//        timesJumped++;
    }
}
