using UnityEngine;

/** Player fast fall script
*/
namespace Player
{
    public class FastFall : AbstractBehavior
    {
        public float pullSpeed = -300;

        void Update()
        {
            var down = _inputState.GetButtonValue(inputButtons[0]);

            if (down && !_collisionState.standing && !_collisionState.onWall)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, pullSpeed);
            }
        }
    }
}