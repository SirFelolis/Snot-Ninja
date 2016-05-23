using UnityEngine;
using System.Collections;

namespace Player
{
    public class GrapplingBallRotateBehavior : MonoBehaviour
    {
        private Rigidbody2D _rb2d;

        void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 dir = _rb2d.velocity;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}