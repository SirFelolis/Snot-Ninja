using UnityEngine;

/** Player jump script
 * Dear lord save me from these comments
 * I'll get rid of them some day
 * Some day...
*/

namespace Player
{
    public class Jump : AbstractBehavior
    {
        [Header("Jumping Fields")]

        public float jumpSpeed = 160.0f;
        public float jumpDelay = .2f;
        public int jumpCount = 2;

        protected float lastJumpTime = 0;
        [HideInInspector]
        public int jumpsRemaining = 0;

        private int timesJumped;
        private bool canJump;
        private bool jumpReleased;
        private float holdTime;

        void Update()
        {
            ToggleScripts(true);

            jumpReleased = Input.GetButtonUp("Jump");
            canJump = _inputState.GetButtonValue(inputButtons[0]);
            holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

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

            if (jumpReleased && _rb2d.velocity.y > 0)
            {
                Vector2 vel = _rb2d.velocity;
                vel.y /= 2f;
                _rb2d.velocity = vel;
            }

        }

        protected virtual void OnJump()
        {
            ToggleScripts(false);
            if (jumpsRemaining == 1)
            {
                var vel = _rb2d.velocity;
                lastJumpTime = Time.time;
                _rb2d.velocity = new Vector2(vel.x, jumpSpeed / 1.3f);
            }
            else
            {
                var vel = _rb2d.velocity;
                lastJumpTime = Time.time;
                _rb2d.velocity = new Vector2(vel.x, jumpSpeed);
            }
        }
    }
}