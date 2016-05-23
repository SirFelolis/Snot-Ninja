using UnityEngine;
using System.Collections;
using Spine.Unity;

namespace Enemy
{
    public class VillagerManager : MonoBehaviour
    {
        #region Inspector
        // Animation names

        [Header("Animation names")]

        [SerializeField]
        [SpineAnimation]
        private string idleName = "idle";

        [SerializeField]
        [SpineAnimation]
        private string runName = "run";

        [SerializeField]
        [SpineAnimation]
        private string walkName = "walk";

        [SerializeField]
        [SpineAnimation]
        private string jumpName = "jump";

        [SerializeField]
        [SpineAnimation]
        private string attackSlice = "attackSlice";
        #endregion

        private SkeletonAnimation anim; // Animator

        private EnemyMove move;
        private EnemyAI AI;
        private EnemyCollisionState collision;

        private string currentAnimation = "";

        void Awake()
        {
            anim = GetComponentInChildren<SkeletonAnimation>();
            move = GetComponent<EnemyMove>();
            AI = GetComponent<EnemyAI>();
            collision = GetComponent<EnemyCollisionState>();
        }

        void FixedUpdate()
        {
            if (collision.standing)
            {
                if (Mathf.Abs(move.Speed) <= 0.1f)
                {
                    SetAnimation(0, idleName, true);
                }
                else if (move.Speed <= AI.PatrolSpeed)
                {
                    SetAnimation(0, walkName, true);
                }
                else if (move.Speed <= AI.ChaseSpeed)
                {
                    SetAnimation(0, runName, true);
                }
            }
            else
            {
                SetAnimation(0, jumpName, false);
            }
        }

        void SetAnimation(int track, string name, bool loop)
        {
            if (currentAnimation == name)
                return;

            anim.state.SetAnimation(track, name, loop);
            currentAnimation = name;
        }
    }
}