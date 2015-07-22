public class StickToWall : AbstractBehavior
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
        if (!_collisionState.standing &&_rb2d.velocity.y > 0)
        {
            _rb2d.gravityScale = 0;
            _rb2d.drag = 100;
        }
    }

    protected virtual void OffWall()
    {
        if (_rb2d.gravityScale != _defaultGravityScale)
        {
            _rb2d.gravityScale = _defaultGravityScale;
            _rb2d.drag = _defaultDrag;
        }
    }
}
