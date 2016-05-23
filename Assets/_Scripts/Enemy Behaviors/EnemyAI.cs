using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

namespace Enemy
{
    public class EnemyAI : AbstractEnemyBehavior
    {
        [SerializeField]
        private float chaseSpeed;
        public float ChaseSpeed
        {
            get { return chaseSpeed; }
            set { return; }
        }

        [SerializeField]
        private float patrolSpeed;
        public float PatrolSpeed
        {
            get { return patrolSpeed; }
            set { return; }
        }

        private EnemyMove enemyMove;

        private SpriteRenderer coneRend;

        [SerializeField]
        private Color chaseColour;

        [SerializeField]
        private Color patrolColour;

        private int prevState = -1;

        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
            enemyMove = GetComponent<EnemyMove>();
            coneRend = GetComponentInChildren<SpriteRenderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (enemySight.PlayerInSight && enemySight.PlayerInMeleeRange) // TODO: Make sure this checks the player health aswell.
                Attack(); // If the player is visible and close enough to attack, do so
            else if (enemySight.PlayerInSight)
                Chase(); // If the player is in sight but not close enough to attack, chase instead
            else
                Patrol(); // If none of the above, patrol instead
            if (prevState != enemyMove.State)
            {
                prevState = enemyMove.State;
                if (enemyMove.State == 0)
                    Camera.main.GetComponent<MotionBlur>().enabled = true;
                else
                    Camera.main.GetComponent<MotionBlur>().enabled = false;
            }
        }

        void Attack()
        {
            enemyMove.Stop();
        }

        void Chase()
        {
            enemyMove.State = 0;
            coneRend.color = chaseColour;
            enemyMove.MaxSpeed = chaseSpeed;
            Camera.main.GetComponent<CameraPosition>().orthoSize = 75;
        }

        void Patrol()
        {
            enemyMove.State = 1;
            coneRend.color = patrolColour;
            enemyMove.MaxSpeed = patrolSpeed;
            Camera.main.GetComponent<CameraPosition>().orthoSize = 100;
        }
    }
}