using UnityEngine;
using System.Collections;

/** Enemy sight state script
*/


public class EnemySightState : AbstractEnemyBehavior
{
    public float sightRadius;
    public float sightRadiusMultiplier;
    public bool vision;
    public LayerMask canSee;

    private float defaultSightRadius;

    protected override void Awake()
    {
        base.Awake();
        defaultSightRadius = sightRadius;
    }

    void Update()
    {
        sightRadius = defaultSightRadius;

        if (vision)
        {
            if (!_enemyFSM.stateLock) _enemyFSM.behavior = EnemyBehaviors.Follow;
            sightRadius *= sightRadiusMultiplier;
        }
        else if (!vision)
            if (!_enemyFSM.stateLock) _enemyFSM.behavior = EnemyBehaviors.Patrol;

        vision = Physics2D.OverlapCircle(transform.position, sightRadius, canSee);
    }

    void OnDrawGizmosSelected()
    {
        if (vision) Gizmos.color = Color.red;
        else
            Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
