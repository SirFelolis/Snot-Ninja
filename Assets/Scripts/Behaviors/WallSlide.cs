using UnityEngine;

public class WallSlide : AbstractBehavior
{
    public bool onWallDetected;

    private float _defaultGravityScale;
    private float _defaultDrag;

    void Start()
    {
        _defaultGravityScale = _rb2d.gravityScale;
        _defaultDrag = _rb2d.drag;
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
        _rb2d.gravityScale = 20.0f;
        Debug.Log("Hey bub");
    }

    void OffWall()
    {
        if (_rb2d.gravityScale != _defaultGravityScale)
        {
            _rb2d.gravityScale = _defaultGravityScale;
        }
    }
}
