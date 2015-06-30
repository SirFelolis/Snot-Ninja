﻿using UnityEngine;
using System.Collections;

public class Friction : AbstractBehavior
{

    void Start()
    {
    }

    void Update()
    {
        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);
        var jump = _inputState.GetButtonValue(inputButtons[2]);
        var down = _inputState.GetButtonValue(inputButtons[3]);

        if ((!left && !right && !jump && _collisionState.standing) || (!left && !right && !jump && down && _collisionState.standing))
        {
            _rb2d.drag = 10000;
        }
        else
        {
            _rb2d.drag = 0;
        }
    }
}
