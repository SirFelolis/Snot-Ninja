using UnityEngine;
using System.Collections;

/** Enemy move script
* WARNING: Possibly the most confusing piece of coding I've ever worked on. (Oliver)
*
* I leave this here as a warning to anyone or even myself in the future, that this code is incredibly confusing. (Oliver)
*/
namespace Enemy
{
    public class EnemyMove : AbstractEnemyBehavior
    {
        private float speed = 50.0f;
        public float Speed
        {
            get { return speed; }
            set { return; }
        }

        [SerializeField]
        private float jumpSpeed = 10;

        private float maxSpeed;
        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        private float defaultSpeed;

        private float dir = 0.0f;
        private float dirLast = 0.0f;

        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        [SerializeField]
        private Directions directions;

        private bool waiting;

        void FixedUpdate()
        {
            if (!waiting)
            {
                if (speed < maxSpeed)
                    speed = Mathf.Lerp(speed, maxSpeed, 0.1f);
                else if (speed > maxSpeed)
                    speed = maxSpeed;
            }

            dir = (int)directions;
            if (dir != dirLast)
            {
                speed = 0;
                dirLast = (int)directions;
            }

            switch (state)
            {
                case 0: // Chase state
                    rb2d.velocity = new Vector2((int)directions * speed, rb2d.velocity.y);

                    if (CheckPath(enemyCollisionState.GroundCollider))
                    {
                        OnJump();
                    }
                    break;

                case 1: // Patrol state
                    rb2d.velocity = new Vector2((int)directions * speed, rb2d.velocity.y);
                    if (CheckPath(enemyCollisionState.GroundCollider) && !waiting) // Weird, there's no layer called "Collider" but this works anyway
                    {
                        StartCoroutine(WaitAndTurn());
                        waiting = true;
                    }
                    else if ((!CheckPath(enemyCollisionState.GroundCollider) && !CheckPath(enemyCollisionState.EdgeCollider)) && !waiting)
                    {
                        StartCoroutine(WaitAndTurn());
                        waiting = true;
                    }
                    break;
                default:
                    rb2d.velocity = new Vector2();
                    break;
            }
            transform.localScale = new Vector3(dir, 1, 1);
        }

        void OnJump()
        {
            if (enemyCollisionState.standing)
            {
                var vel = rb2d.velocity;
                rb2d.velocity = new Vector2(vel.x, jumpSpeed);
            }
        }

        bool CheckPath(BoxCollider2D box)
        {
            return box.IsTouchingLayers(LayerMask.NameToLayer("Collider"));
        }

        void Turn()
        {
            directions = directions == Directions.Right ? Directions.Left : Directions.Right;
        }

        public void Stop()
        {
            speed = 0;
        }

        IEnumerator WaitAndTurn()
        {
            Stop();
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            Turn();
            yield return new WaitForSeconds(0.1f);
            waiting = false;
        }
    }
}