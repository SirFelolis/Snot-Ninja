using UnityEngine;

/** Player manager script
*/

public class PlayerManager : MonoBehaviour
{
    private InputState _inputState;
    private SkeletonAnimation _animator;
    private CollisionState _collisionState;
    private Attack _attackBehavior;
    private Rigidbody2D _rb2d;
    private GrapplingHookBehavior _hook;
    private CrouchBehavior _crouch;

    void Awake()
    {
        _inputState = GetComponent<InputState>();
        _animator = GetComponentInChildren<SkeletonAnimation>();
        _collisionState = GetComponent<CollisionState>();
        _attackBehavior = GetComponent<Attack>();
        _rb2d = GetComponent<Rigidbody2D>();
        _hook = GetComponent<GrapplingHookBehavior>();
        _crouch = GetComponent<CrouchBehavior>();
    }


    void Update()
    {
        _animator.state.TimeScale = 1;
        if (_attackBehavior.attacking)
        {
            ChangeAnimationState("slash_stand", false); // Attacking animation
            //            ChangeAnimationState(4);
        }
        else
        {
            if (_inputState.absVelX < 0.2f && _collisionState.standing && !_crouch.crouching) // Idle animation
            {
                ChangeAnimationState("idle", true);
                //                ChangeAnimationState(0);
            }

            if (_inputState.absVelX > 2.5 && _collisionState.standing) // Running animation
            {
                ChangeAnimationState("run", true);
                _animator.state.TimeScale = _inputState.absVelX / 90;
                //                ChangeAnimationState(1);
            }

            if (_rb2d.velocity.y > 50f && !_collisionState.standing && !_hook.attached) // Jumping animation
            {
                ChangeAnimationState("jump", false);
                //                ChangeAnimationState(2);
            }

            if (_rb2d.velocity.y < -50.0f && !_collisionState.standing && !_hook.attached) // Falling animation
            {
                ChangeAnimationState("fall", true);
                //                ChangeAnimationState(3);
            }

            if (_crouch.crouching) // Crouching animation
            {
                ChangeAnimationState("crouching", false);
            }
        }
    }
    void ChangeAnimationState(string animationName, bool loop)
    {
        const int TRACK = 0;
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
