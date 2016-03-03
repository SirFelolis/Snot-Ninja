using UnityEngine;

/** Player slide script
*/

public class Slide : AbstractBehavior {

    public float scale = 0.5f;
    public bool sliding;
    public float centerOffsetY = 0;

    private CircleCollider2D _circleCollider;
    private Vector2 _originalCenter;

    protected override void Awake()
    {
        base.Awake();
        _circleCollider = GetComponent<CircleCollider2D>();
        _originalCenter = _circleCollider.offset;
    }

    protected virtual void OnSlide(bool value)
    {
        sliding = value;

        ToggleScripts(!sliding);

        var size = _circleCollider.radius;

        float newOffsetY;
        float sizeReciprocal;

        if (sliding)
        {
            sizeReciprocal = scale;
            newOffsetY = _circleCollider.offset.y - size / 2 + centerOffsetY;
        }
        else
        {
            sizeReciprocal = 1 / scale;
            newOffsetY = _originalCenter.y;
        }

        size = size * sizeReciprocal;
        _circleCollider.radius = size;
        _circleCollider.offset = new Vector2(_circleCollider.offset.x, newOffsetY);
    }

    void Update()
    {
        var canSlide = _inputState.GetButtonValue(inputButtons[0]);
        if (canSlide && _collisionState.standing && !sliding)
        {
            OnSlide(true);
        }
        else if (sliding && !canSlide)
        {
            OnSlide(false);
        }
    }
}
