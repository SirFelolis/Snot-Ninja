﻿using UnityEngine;
using System.Collections;

/** Enemy abstract behavior script
*/


public abstract class AbstractEnemyBehavior : MonoBehaviour
{
    public MonoBehaviour[] disableScripts;

    protected GameObject _player;
    protected Rigidbody2D _rb2d;
    protected EnemyCollisionState _enemyCollisionState;
    protected EnemyFSM _enemyFSM;

    protected Directions directions;

    protected virtual void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _rb2d = GetComponent<Rigidbody2D>();
        _enemyCollisionState = GetComponent<EnemyCollisionState>();
        _enemyFSM = GameObject.FindObjectOfType<EnemyFSM>();
    }

    protected virtual void ToggleScripts(bool value)
    {
        foreach (var script in disableScripts)
        {
            script.enabled = value;
        }
    }

}