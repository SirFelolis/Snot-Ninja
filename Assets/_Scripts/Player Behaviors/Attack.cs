using UnityEngine;
using System.Collections;

public class Attack : AbstractBehavior
{
    public bool attacking;
    public GameObject attackBox;

    private bool attackingWhileMoving;

    private float attackTime;

    void FixedUpdate()
    {
        var attackKey = Input.GetMouseButtonDown(0);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (attackKey && _collisionState.standing && holdTime < 0.1f)
        {
            attacking = true;
            if (_inputState.absVelX > 0)
            {
                attackingWhileMoving = true;
            }
        }

        if (attackTime > 0.3f)
        {
            attacking = false;
            attackingWhileMoving = false;
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

        if (attacking && !attackingWhileMoving)
        {
            ToggleScripts(false);
        }
        else
        {
            ToggleScripts(true);
        }
    }
}