using UnityEngine;

/** Player manager script
*/

public class PlayerManager : MonoBehaviour
{
    private InputState _inputState;
    private Animator _animator;
    private CollisionState _collisionState;
    private Attack _attackBehavior;

    void Awake()
    {
        _inputState = GetComponent<InputState>();
        _animator = GetComponent<Animator>();
        _collisionState = GetComponent<CollisionState>();
        _attackBehavior = GetComponent<Attack>();
    }

    void Update()
    {
        if (_collisionState.standing) // Idle animation
        {
            ChangeAnimationState(0);
        }

        if (_inputState.absVelX > 0.2f) // Running animation
        {
            ChangeAnimationState(1);
        }

        if (_inputState.absVelY > 0.5f) // Jumping animation
        {
            ChangeAnimationState(2);
        }

        if (_attackBehavior.attacking) // Attacking animation
        {
            ChangeAnimationState(3);
        }

        if (!_collisionState.standing && _collisionState.onWall) // Wall slide animation
        {
            ChangeAnimationState(4);
        }
    }

    void ChangeAnimationState(int value)
    {
        _animator.SetInteger("AnimState", value);
    }
}
