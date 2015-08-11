using UnityEngine;
using System.Collections;

public enum EnemyBehaviors
{
    Follow,
    Patrol,
    Stunned
};

public class EnemyFSM : AbstractEnemyBehavior
{
    public bool stateLock;

    public EnemyBehaviors behavior;
}
