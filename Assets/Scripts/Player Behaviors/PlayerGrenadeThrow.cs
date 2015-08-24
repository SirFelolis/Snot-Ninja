using UnityEngine;
using System.Collections;

public class PlayerGrenadeThrow : AbstractBehavior
{
    public float throwSpeed;

    void Update()
    {
        var use = _inputState.GetButtonValue(inputButtons[0]);
    }
}
