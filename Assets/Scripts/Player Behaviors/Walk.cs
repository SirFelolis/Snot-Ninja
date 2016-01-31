using UnityEngine;

/** Player walk script
*/

public class Walk : AbstractBehavior
{
    [Header("Movement Fields")]
    public float speed = 50.0f;
    public float runMultiplier = 1.0f;
    public float walkMultiplier = 0.5f;
    public float maxSpeed;
    public bool moving;
    public bool slowMoving;
    public bool skidding = false;

    void FixedUpdate()
    {
        moving = false;
        slowMoving = false;

        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);
        var walk = _inputState.GetButtonValue(inputButtons[2]);

        if (_collisionState.standing)
        {
            if (right || left)
            {
                var currentSpeed = speed;

                if (walk)
                {
                    currentSpeed *= walkMultiplier;
                    slowMoving = true;
                }

                moving = true;

                var velX = currentSpeed * Input.GetAxis("Horizontal");

                _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && _inputState.absVelX > 0)
            {
                skidding = true;
            }
            else
            {
                skidding = false;
            }
        }
        else
        {
            var currentSpeed = speed / 15.0f;

            moving = true;

            var velX = Mathf.Lerp(
                _rb2d.velocity.x,
                Mathf.Clamp(
                    _rb2d.velocity.x,
                    -maxSpeed,
                    maxSpeed),
                0.5f);

            _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
            _rb2d.velocity += new Vector2(currentSpeed * Input.GetAxis("Horizontal"), 0);
        }

        if (!moving && _collisionState.standing)
            _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
    }
}
