using UnityEngine;
using System.Collections;

public class Attack : AbstractBehavior
{
    public bool attacking;

    private float attackTime;

    void FixedUpdate()
    {
        var attackKey = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (attackKey && _collisionState.standing && holdTime < 0.1f && _inputState.absVelX < 1f)
        {
            attacking = true;
        }

        if (attackTime > 0.3f)
        {
            attacking = false;
        }

        if (attacking)
        {
            attackTime += Time.deltaTime;
        }
        else
        {
            attackTime = 0;
        }
    }
}