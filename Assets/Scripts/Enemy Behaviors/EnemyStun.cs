using UnityEngine;
using System.Collections;

public class EnemyStun : AbstractEnemyBehavior
{
    public float stunTime = 2.0f;
    public bool stunned;

    private float defaultStunTime;

    protected override void Awake()
    {
        base.Awake();
        defaultStunTime= stunTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttackTrigger"))
        {
            if (!_enemyFSM.stateLock) _enemyFSM.behavior = EnemyBehaviors.Stunned;
            stunned = true;
            _enemyFSM.stateLock = true;
            _rb2d.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (stunned)
        {
            stunTime -= Time.deltaTime;
        }

        if (stunTime <= 0 && stunned)
        {
            stunned = false;
            _enemyFSM.stateLock = false;
            stunTime = defaultStunTime;
        }
    }

}
