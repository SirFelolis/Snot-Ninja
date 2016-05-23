using UnityEngine;
using System.Collections;

namespace Player
{
    public class HookLineBehavior : MonoBehaviour
    {
        public Transform playerPos;

        private GrapplingHookBehavior _grapple;

        void Awake()
        {
            playerPos = GameObject.Find("Nose").transform;
            _grapple = GameObject.FindGameObjectWithTag("Player").GetComponent<GrapplingHookBehavior>();
        }

        void FixedUpdate()
        {
            Vector3 vectorToTarget = playerPos.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 1000.0f);

            var scale = transform.localScale;

            scale.x = Vector3.Distance(transform.position, playerPos.position) / 72.8f;

            transform.localScale = scale;

            if (!_grapple.attached)
            {
                Destroy(gameObject);
            }
        }
    }
}