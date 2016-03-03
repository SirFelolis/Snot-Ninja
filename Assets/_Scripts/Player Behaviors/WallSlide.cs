using UnityEngine;

/** Player wall slide script
*/

public class WallSlide : AbstractBehavior
{
    public bool onWallDetected;

    private float _defaultGravityScale;

    void Start()
    {
        _defaultGravityScale = _rb2d.gravityScale;
    }

    void Update()
    {

        if (_collisionState.onWall)
        {
            if (!onWallDetected)
            {
                onWallDetected = true;
                OnStick();
                ToggleScripts(false);
            }
        }
        else
        {
            if (onWallDetected)
            {
                OffWall();
                ToggleScripts(true);
                onWallDetected = false;
            }
        }
    }

    void OnStick()
    {
        _rb2d.gravityScale = 25.0f;
    }

    void OffWall()
    {
        if (_rb2d.gravityScale != _defaultGravityScale)
        {
            _rb2d.gravityScale = _defaultGravityScale;
        }
    }
}
