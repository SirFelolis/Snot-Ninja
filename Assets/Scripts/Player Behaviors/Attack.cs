using UnityEngine;
using System.Collections;

public class Attack : AbstractBehavior
{
    public bool attacking;

    void Update()
    {
        attacking = _inputState.GetButtonValue(inputButtons[0]);
        var attackTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (attackTime > 1.0f)
        {
            attacking = false;
        }
    }
}