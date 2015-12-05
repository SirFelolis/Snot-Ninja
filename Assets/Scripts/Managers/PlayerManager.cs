using UnityEngine;

/** Player manager script
*/

public class PlayerManager : MonoBehaviour
{
    private InputState _inputState;
    private Animator _animator;
    private CollisionState _collisionState;
    private Attack _attackBehavior;
    private Rigidbody2D _rb2d;

    void Awake()
    {
        _inputState = GetComponent<InputState>();
        _animator = GetComponentInChildren<Animator>();
        _collisionState = GetComponent<CollisionState>();
        _attackBehavior = GetComponent<Attack>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_attackBehavior.attacking)
        {
            if (_collisionState.standing) // Idle animation
            {
                ChangeAnimationState(0);
            }

            if (_inputState.absVelX > 2.5) // Running animation
            {
                ChangeAnimationState(1);
            }

            if (_rb2d.velocity.y > 0.5f && !_collisionState.standing) // Jumping animation
            {
                ChangeAnimationState(2);
            }

            if (_rb2d.velocity.y < 0.0f && !_collisionState.standing)
            {
                ChangeAnimationState(4);
            }
        }
    }

    void ChangeAnimationState(int value)
    {
        _animator.SetInteger("AnimState", value);
    }
}
