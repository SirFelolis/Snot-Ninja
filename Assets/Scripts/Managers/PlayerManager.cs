using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    private InputState _inputState;
    private Walk _walkBehavior;
    private Animator _animator;
    private CollisionState _collisionState;
    private Duck _duckBehavior;

    void Awake ()
    {
        _inputState = GetComponent<InputState>();
        _walkBehavior = GetComponent<Walk>();
        _animator = GetComponent<Animator>();
        _collisionState = GetComponent<CollisionState>();
        _duckBehavior = GetComponent<Duck>();
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

        if (_duckBehavior.ducking) // Ducking animation
        {
            ChangeAnimationState(3);
        }

        if (!_collisionState.standing && _collisionState.onWall) // Wall slide animation
        {
            ChangeAnimationState(4);
        }

        _animator.speed = _walkBehavior.running ? _walkBehavior.runMultiplier : 1;
    }

    void ChangeAnimationState(int value)
    {
        _animator.SetInteger("AnimState", value);
    }
}
