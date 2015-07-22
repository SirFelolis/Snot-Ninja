using UnityEngine;

public class WallSlide : StickToWall
{
    public float slideVelocity = -5.0f;
    public float slideMultiplier = 5.0f;

    protected override void Update()
    {
        base.Update();

        if (onWallDetected && !_collisionState.standing)
        {
            var velY = slideVelocity;

            if (_inputState.GetButtonValue(inputButtons[0]))
            {
                velY *= slideMultiplier;
            }

            _rb2d.velocity = new Vector2(_rb2d.velocity.x, velY);
        }
    }

    protected override void OnStick()
    {
        _rb2d.velocity = Vector2.zero;
    }

    protected override void OffWall()
    {
        // Does nothing
    }
}
