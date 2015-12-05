using UnityEngine;

/** Player walk script
*/

public class Walk : AbstractBehavior
{
    [Header("Movement Fields")]
    public float speed = 50.0f;
    public float runMultiplier = 1.0f;
    public float maxSpeed;
    public bool moving;

    void FixedUpdate()
    {
        moving = false;

        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);

        if (_collisionState.standing)
        {
            if (right || left)
            {
                var currentSpeed = speed;

                moving = true;

                var velX = currentSpeed * Input.GetAxis("Horizontal");

                _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
            }
        }
        else
        {
            var currentSpeed = speed / 20.0f;

            moving = true;

            _rb2d.velocity = new Vector2(Mathf.Lerp(_rb2d.velocity.x, Mathf.Clamp(_rb2d.velocity.x, -maxSpeed, maxSpeed), 0.5f), _rb2d.velocity.y);
            _rb2d.velocity += new Vector2(currentSpeed * Input.GetAxis("Horizontal"), 0);
        }

        if (!moving && _collisionState.standing)
            _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
    }
}
