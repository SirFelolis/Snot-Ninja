using UnityEngine;
using System.Collections;

/** Grenade rotation script
 * Causes the grenade to rotate while in the air.
*/

namespace PixelArtRotation
{
    public class GrenadeRotate : MonoBehaviour
    {
        public int rotationSpeed;
        public bool grounded;
        public LayerMask whatIsGround;

        private PixelRotation _pixelRotation;

        void Awake()
        {
            _pixelRotation = GetComponent<PixelRotation>();
        }

        void Update()
        {
            grounded = Physics2D.OverlapCircle(transform.position, 4, whatIsGround);

            if (!grounded)
            {
                _pixelRotation.Angle += rotationSpeed;
            }
            else
                _pixelRotation.Angle = 0;
        }
    }
}
