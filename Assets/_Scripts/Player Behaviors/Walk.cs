using UnityEngine;

/** Player walk script
*/

namespace Player
{
    public class Walk : AbstractBehavior
    {
        [Header("Movement Fields")]
        public float speed = 110.0f;
        public float walkSpeed = 50.0f;
        public float maxSpeed;
        public bool moving;
        public bool slowMoving;
        public bool skidding = false;

        void FixedUpdate()
        {
            moving = false;
            slowMoving = false;
            skidding = false;

            var right = _inputState.GetButtonValue(inputButtons[0]);
            var left = _inputState.GetButtonValue(inputButtons[1]);
            var walk = _inputState.GetButtonValue(inputButtons[2]);

            if (_collisionState.standing) // Movement
            {
                if (right || left)
                {
                    var currentSpeed = speed;

                    if (walk)
                    {
                        currentSpeed = walkSpeed;
                        slowMoving = true;
                    }

                    moving = true;

                    var velX = currentSpeed * Input.GetAxis("Horizontal");

                    _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
                }

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 1 && _inputState.absVelX > 5) // Skidding
                {
                    skidding = true;
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
}