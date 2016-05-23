using UnityEngine;

/** Player face direction script
*/

namespace Player
{
    public class FaceDirection : AbstractBehavior
    {
        void Update()
        {
            var right = _inputState.GetButtonValue(inputButtons[0]);
            var left = _inputState.GetButtonValue(inputButtons[1]);

            if (right)
            {
                _inputState.direction = Directions.Right;
            }
            else if (left)
            {
                _inputState.direction = Directions.Left;
            }

            transform.localScale = new Vector3((float)_inputState.direction, 1, 1);

        }
    }
}