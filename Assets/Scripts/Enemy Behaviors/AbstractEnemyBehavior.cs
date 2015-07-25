using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyBehavior : MonoBehaviour
{
    public MonoBehaviour[] disableScripts;

    protected Rigidbody2D _rb2d;
    protected EnemyCollisionState _enemyCollisionState;

    protected Directions directions;

    protected virtual void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _enemyCollisionState = GetComponent<EnemyCollisionState>();
    }

    protected virtual void ToggleScripts(bool value)
    {
        foreach (var script in disableScripts)
        {
            script.enabled = value;
        }
    }

}
