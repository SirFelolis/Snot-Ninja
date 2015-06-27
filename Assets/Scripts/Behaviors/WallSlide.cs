using UnityEngine;
using System.Collections;

public class WallSlide : StickToWall
{
    public float slideVelocity = -5.0f;
    public float slideMultiplier = 5.0f;

    protected override void Update()
    {
        base.Update();
    }
}
