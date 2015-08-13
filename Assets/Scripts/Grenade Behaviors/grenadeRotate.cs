using UnityEngine;
using System.Collections;

namespace PixelArtRotation
{
    public class grenadeRotate : MonoBehaviour
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
