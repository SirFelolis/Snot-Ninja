public class FastFall : AbstractBehavior
{
    public float gravityMultiplier = 5.0f;
    private float defaultGravityScale;

    void Start()
    {
        defaultGravityScale = _rb2d.gravityScale;
    }

    void Update()
    {

        var down = _inputState.GetButtonValue(inputButtons[0]);

        if (_rb2d.gravityScale > defaultGravityScale)
            _rb2d.gravityScale = defaultGravityScale;

        if (down && !_collisionState.standing && !_collisionState.onWall)
        {
            _rb2d.gravityScale *= gravityMultiplier;
        }
    }
}
