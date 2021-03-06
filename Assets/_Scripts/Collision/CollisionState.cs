﻿using UnityEngine;
using System.Collections;

/** Player collision state script
*/

namespace Player
{
    public class CollisionState : MonoBehaviour
    {
        public LayerMask collisionLayer;
        public bool standing;
        public bool onWall;
        public bool onCeil;
        public Vector2 bottomPosition = Vector2.zero;
        public Vector2 rightPosition = Vector2.zero;
        public Vector2 leftPosition = Vector2.zero;
        public Vector2 topPosition = Vector2.zero;
        public float collisionRadius = 10.0f;
        public Color debugCollisionColor = Color.red;

        private InputState _inputState;

        void Awake()
        {
            _inputState = GetComponent<InputState>();
        }

        void FixedUpdate()
        {
            var pos = bottomPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

            pos = _inputState.direction == Directions.Right ? leftPosition : rightPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onWall = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

            pos = topPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onCeil = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = debugCollisionColor;

            var positions = new Vector2[] { rightPosition, bottomPosition, leftPosition, topPosition };

            foreach (var position in positions)
            {
                var pos = position;
                pos.x += transform.position.x;
                pos.y += transform.position.y;

                Gizmos.DrawWireSphere(pos, collisionRadius);
            }
        }
    }
}