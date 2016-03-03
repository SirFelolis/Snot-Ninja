using UnityEngine;
using System.Collections;

public class PlayerHurtBehavior : AbstractBehavior
{
    public bool stunned = false;
    public bool getUp = false;

    public float stunTime;

    private float time;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _rb2d.velocity = new Vector2(150 * (int)_inputState.direction, 100);
            stunned = true;
        }
    }

    void FixedUpdate()
    {
        if (stunned && time < stunTime)
        {
            if (time >= stunTime / 10)
            {
                getUp = true;
            }
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            stunned = false;
            getUp = false;
        }

        if (stunned)
        {
            ToggleScripts(false);
        }
        else
        {
            ToggleScripts(true);
        }
    }

}
