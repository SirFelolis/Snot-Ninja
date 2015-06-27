﻿using UnityEngine;
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
        if (_collisionState.standing)
        {
            ChangeAnimationState(0);
        }

        if (_inputState.absVelX > 0.2f)
        {
            ChangeAnimationState(1);
        }

        if (_inputState.absVelY > 0.5f)
        {
            ChangeAnimationState(2);
        }

        if (_duckBehavior.ducking)
        {
            ChangeAnimationState(3);
        }

        if (!_collisionState.standing && _collisionState.onWall)
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