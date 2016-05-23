using UnityEngine;
using Spine.Unity;
using Spine;

/** Player manager script
*/

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region Inspector
        // Animation names

        [Header("Animation names")]

        [SerializeField]
        [SpineAnimation]
        private string crouchName = "crouching";

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
        private string fallName = "fall";

        [SerializeField]
        [SpineAnimation]
        private string attackName = "slash_stand";

        [SerializeField]
        [SpineAnimation]
        private string runAttackName = "slash_run";

        [SerializeField]
        [SpineAnimation]
        private string jumpAttackName = "slash_jump";

        // Audio vars
        [Space(10)]

        [Header("Audio & Sounds")]

        [SerializeField]
        private AudioClip[] clips;

        #endregion

        private AudioSource source;

        // Animation references

        private SkeletonAnimation anim; // Animator

        private InputState inputState;
        private CollisionState collisionState;
        private Attack attackBehavior;
        private Rigidbody2D rb2d;
        private CrouchBehavior crouchBehavior;

        private string currentAnimation = "";

        void Awake()
        {
            inputState = GetComponent<InputState>();
            anim = GetComponentInChildren<SkeletonAnimation>();
            collisionState = GetComponent<CollisionState>();
            attackBehavior = GetComponent<Attack>();
            rb2d = GetComponent<Rigidbody2D>();
            crouchBehavior = GetComponent<CrouchBehavior>();
        }

        void FixedUpdate()
        {
            // ----AUDIO----

            // Player sound effects here. Footsteps, breathing etc

            // ----ANIMATIONS----

            if (collisionState.standing)
            {
                if (crouchBehavior.crouching)
                {
                    SetAnimation(0, crouchName, true); // Crouch animation
                }
                else
                {
                    if (inputState.absVelX < 1.8)
                    {
                        if (attackBehavior.Attacking)
                        {
                            SetAnimation(0, attackName, false); // Attack standing animation
                        }
                        else
                        {
                            SetAnimation(0, idleName, true); // Idle animation
                        }
                    }
                    else
                    {
                        if (attackBehavior.Attacking)
                        {
                            SetAnimation(1, runAttackName, false); // Run attack animation
                        }
                        SetAnimation(0, inputState.absVelX > 52.0f ? runName : walkName, true); // Walking/running animation
                    }
                }
            }
            else
            {
                if (attackBehavior.Attacking)
                    SetAnimation(0, jumpAttackName, false); // Attacking in air
                else if (rb2d.velocity.y > 0)
                    SetAnimation(0, jumpName, false); // Jumping
                else
                    SetAnimation(0, fallName, true); // Falling
            }
        }

        void SetAnimation(int track, string name, bool loop)
        {
            if (currentAnimation == name)
                return;

            anim.state.SetAnimation(track, name, loop);
            currentAnimation = name;
        }

        void AddAnimation(int track, string name, bool loop, float delay)
        {
            if (currentAnimation == name)
                return;

            anim.state.AddAnimation(track, name, loop, delay);
            currentAnimation = name;
        }
    }
}