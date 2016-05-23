using UnityEngine;
using System.Collections;

/** Grappling hook script
*/

namespace Player
{
    public class GrapplingHookBehavior : AbstractBehavior
    {
        public LayerMask whatIsGround;

        public bool canGrapple;
        public bool shooting;
        public bool aiming;
        public float ballSpeed;

        public float timesShot = 0;
        public float hookLimit = 2;

        public float coolDown = 0.5f;
        private float time;

        public GameObject ball;
        public GameObject ballHit;
        public GameObject hookLine;

        private Jump jumpBehavior;

        [HideInInspector]
        public bool attached = false;
        [HideInInspector]
        public Vector2 attachedPoint = new Vector2();

        private Transform nose;

        protected override void Awake()
        {
            base.Awake();

            jumpBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>();
            nose = GameObject.Find("Nose").transform;
        }

        void FixedUpdate()
        {
            if (time <= 0)
            {
                canGrapple = true;
            }
            else
            {
                time -= Time.deltaTime;
                canGrapple = false;
            }
        }

        void Update()
        {
            //        var shootHook = _inputState.GetButtonValue(inputButtons[0]);
            var shootHook = Input.GetMouseButtonDown(1);
            var jump = _inputState.GetButtonValue(inputButtons[0]);
            //        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

            if (shootHook && canGrapple)
            {
                if (attached)
                {
                    OnGrapplingHookDetach();
                    return;
                }

                if (timesShot > hookLimit)
                    return;


                OnShootHook();
                shooting = true;
                time = coolDown;
            }

            if (attached && shooting)
            {
                timesShot++;
                shooting = false;
                OnGrapplingHookAttach();
            }

            if (attached)
            {
                Attached();
            }

            if (jump && attached)
            {
                OnGrapplingHookDetach();
            }

            if (_collisionState.standing)
            {
                timesShot = 0;
            }
        }

        void OnShootHook()
        {
            var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = (worldMousePosition - transform.position).normalized;

            GameObject obj = (GameObject)Instantiate(ball, nose.position, transform.rotation); // Ball
            obj.GetComponent<Rigidbody2D>().velocity = direction * ballSpeed;
        }

        void OnGrapplingHookAttach() // Attach grappling hook
        {
            Instantiate(ballHit, attachedPoint, transform.rotation);
            Instantiate(hookLine,
                attachedPoint,
                Quaternion.AngleAxis(
                    Mathf.Atan2(
                        (nose.position - (Vector3)attachedPoint).y,
                        (nose.position - (Vector3)attachedPoint).x) * Mathf.Rad2Deg,
                        Vector3.forward)); // Line

            attached = true;
            ToggleScripts(false);
        }

        void Attached()
        {
            Vector2 targetVel = ((Vector3)attachedPoint - transform.position).normalized * 370;
            _rb2d.velocity += (targetVel - _rb2d.velocity) * 0.25f;
        }

        void OnGrapplingHookDetach() // Detach grappling hook
        {
            if (jumpBehavior.jumpsRemaining < 1)
                jumpBehavior.jumpsRemaining++;
            if (attached)
            {
                attached = false;
                ToggleScripts(true);
            }
        }
    }
}