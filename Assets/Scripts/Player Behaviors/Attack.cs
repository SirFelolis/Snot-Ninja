using UnityEngine;
using System.Collections;

public class Attack : AbstractBehavior
{
    public bool attacking;
    public GameObject attackBox;

    private float attackTime;

    void FixedUpdate()
    {
        var attackKey = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (attackKey && _collisionState.standing && holdTime < 0.1f && _inputState.absVelX < 1f)
        {
            attacking = true;
            ToggleScripts(false);
        }

        if (attackTime > 0.3f)
        {
            attacking = false;
            ToggleScripts(true);
        }

        if (attacking)
        {
            attackBox.SetActive(true);
            attackTime += Time.deltaTime;
        }
        else
        {
            attackBox.SetActive(false);
            attackTime = 0;
        }
    }
}