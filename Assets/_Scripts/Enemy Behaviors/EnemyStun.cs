using UnityEngine;
using System.Collections;

namespace Enemy
{
    public class EnemyStun : AbstractEnemyBehavior
    {
        public float stunTime = 2.0f;
        public bool stunned;

        private float defaultStunTime;

        protected override void Awake()
        {
            base.Awake();
            defaultStunTime = stunTime;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttackTrigger"))
            {
                rb2d.velocity = Vector2.zero;
            }
        }

        void FixedUpdate()
        {
            ToggleScripts(true);
            if (stunned)
            {
                ToggleScripts(false);
                stunTime -= Time.deltaTime;
            }

            if (stunTime <= 0 && stunned)
            {
                stunned = false;
                stunTime = defaultStunTime;
            }
        }

    }
}