using UnityEngine;

public class WallSlide : AbstractBehavior
{
    public bool onWallDetected;

    protected float _defaultGravityScale;
    protected float _defaultDrag;

    void Start()
    {
        _defaultGravityScale = _rb2d.gravityScale;
        _defaultDrag = _rb2d.drag;
    }

    protected virtual void Update()
    {
        if (_collisionState.onWall)
        {
            if (!onWallDetected)
            {
                OnStick();
                ToggleScripts(false);
                onWallDetected = true;
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

    protected virtual void OnStick()
    {
        _rb2d.gravityScale = 20.0f;
        Debug.Log("Hey");
    }

    protected virtual void OffWall()
    {
        if (_rb2d.gravityScale != _defaultGravityScale)
        {
            _rb2d.gravityScale = _defaultGravityScale;
            Debug.Log("Ho");
        }
    }
}
