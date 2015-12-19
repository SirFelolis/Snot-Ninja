using UnityEngine;

/** Player manager script
*/

public class PlayerManager : MonoBehaviour
{
    private InputState _inputState;
    private Animator _animator;
    private SkeletonAnimator _skeleton;
    private CollisionState _collisionState;
    private Attack _attackBehavior;
    private Rigidbody2D _rb2d;
    private GrapplingHookBehavior _hook;

    void Awake()
    {
        _inputState = GetComponent<InputState>();
        _animator = GetComponentInChildren<Animator>();
        _skeleton = GetComponentInChildren<SkeletonAnimator>();
        _collisionState = GetComponent<CollisionState>();
        _attackBehavior = GetComponent<Attack>();
        _rb2d = GetComponent<Rigidbody2D>();
        _hook = GetComponent<GrapplingHookBehavior>();
    }

    void Update()
    {
        _skeleton.skeleton.SetBonesToSetupPose();
        if (!_attackBehavior.attacking)
        {
            if (_collisionState.standing) // Idle animation
            {
                ChangeAnimationState(0);
            }

            if (_inputState.absVelX > 2.5 && _collisionState.standing) // Running animation
            {
                ChangeAnimationState(1);
            }

            if (_rb2d.velocity.y > 1f && !_collisionState.standing && !_hook.isGrappled) // Jumping animation
            {
                ChangeAnimationState(2);
            }

            if (_rb2d.velocity.y < 0.0f && !_collisionState.standing) // Falling animation
            {
                ChangeAnimationState(3);
            }
        }
        else
        {
            ChangeAnimationState(4); // Attacking animation
        }
    }

    void ChangeAnimationState(int value)
    {
        _animator.SetInteger("AnimState", value);
    }
}
