using UnityEngine;

/** Player manager script
*/

public class PlayerManager : MonoBehaviour
{
    // Audio vars
    public AudioClip[] clips;
    private AudioSource source;

    private bool prevStanding;

    // Animation vars
    
    private InputState _inputState;
    private SkeletonAnimation _animator;
    private CollisionState _collisionState;
    private Attack _attackBehavior;
    private Rigidbody2D _rb2d;
    private GrapplingHookBehavior _hook;
    private CrouchBehavior _crouch;
    private Walk _walk;
    private SlopeBehavior _slope;

    void Awake()
    {
        _inputState = GetComponent<InputState>();
        _animator = GetComponentInChildren<SkeletonAnimation>();
        _collisionState = GetComponent<CollisionState>();
        _attackBehavior = GetComponent<Attack>();
        _rb2d = GetComponent<Rigidbody2D>();
        _hook = GetComponent<GrapplingHookBehavior>();
        _crouch = GetComponent<CrouchBehavior>();
        _walk = GetComponent<Walk>();
        _slope = GetComponent<SlopeBehavior>();
    }

    void Update()
    {
        // ----AUDIO----

        // ...

        // ----ANIMATIONS----

        _animator.state.TimeScale = 1;
        if (_attackBehavior.Attacking && !_walk.moving)
        {
            ChangeAnimationState("slash_stand", false, 0); // Attacking animation
        }
        else
        {
            if ((_inputState.absVelX < 2f || _slope.slidingDownSlope) && _collisionState.standing && !_crouch.crouching) // Idle animation
            {
                ChangeAnimationState("idle", true, 0);
            }

            if (_inputState.absVelX > 5 && _collisionState.standing && !_walk.skidding) // Running animation
            {
                if (_walk.slowMoving)
                {
                    ChangeAnimationState("walk", true, 0);
                    _animator.state.TimeScale = _inputState.absVelX / 30;
                }
                else
                {
                    ChangeAnimationState("run", true, 0);
                    _animator.state.TimeScale = _inputState.absVelX / 90;
                }
            }

            if (_attackBehavior.Attacking && _walk.moving) // Attacking while moving animation
            {
                ChangeAnimationState("slash_run", false, 1);
            }

            if (_walk.skidding && _collisionState.standing && !_slope.slidingDownSlope) // Skidding animation
            {
                ChangeAnimationState("run_stop", false, 0);
            }

            if (_rb2d.velocity.y > 50f && !_collisionState.standing && !_hook.attached) // Jumping animation
            {
                ChangeAnimationState("jump", false, 0);
            }

            if (_rb2d.velocity.y < -50.0f && !_collisionState.standing && !_hook.attached) // Falling animation
            {
                ChangeAnimationState("fall", true, 0);
            }

            if (_crouch.crouching) // Crouching animation
            {
                ChangeAnimationState("crouching", false, 0);
            }
        }
    }

    void ChangeAnimationState(string animationName, bool loop, int TRACK)
    {
        var state = _animator.state; if (state == null) return;
        var current = state.GetCurrent(TRACK);
        if (current == null || current.Animation.Name != animationName)
        {
            state.SetAnimation(TRACK, animationName, loop);
            _animator.skeleton.SetBonesToSetupPose();
            state.Start += delegate { _animator.skeleton.SetToSetupPose(); };
        }
    }
}
