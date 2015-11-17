using UnityEngine;
using System.Collections;

/** Grappling hook script
*/

public class GrapplingHookBehavior : AbstractBehavior
{
    public LayerMask whatIsGround;

    protected SpringJoint2D _spring;
    private bool _attached = false;

    protected override void Awake()
    {
        base.Awake();
        _spring = GetComponent<SpringJoint2D>();
    }

	void FixedUpdate()
	{
        var shootHook = _inputState.GetButtonValue(inputButtons[0]);

        if (shootHook && !_collisionState.standing && !_attached)
        {
            OnGrapplingHookAttach();
        }

        if (_attached)
        {
            OnGrapplingHookAttached();
        }

        if ((!shootHook || _collisionState.onWall || _collisionState.standing) && _attached)
        {
            OnGrapplingHookDetach();
        }
    }

    Vector3 SetGrapplingPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(45.0f * -(float)_inputState.direction, 45.0f), Mathf.Infinity, whatIsGround); // TODO: Should not have infinite reach.
        Vector3 grapplePoint = hit.point;

        grapplePoint = hit.point;
        return grapplePoint;
    }

    void OnGrapplingHookAttach() // Initialize grappling hook
    {
        _spring.connectedAnchor = SetGrapplingPoint();
        _spring.distance = (transform.position - (Vector3)_spring.connectedAnchor).magnitude;
        _spring.enabled = true;
        _attached = true;
        ToggleScripts(false);
    }

    void OnGrapplingHookAttached() // Run every frame the grappling hook is used
    {
        var up = _inputState.GetButtonValue(inputButtons[1]);
        var down = _inputState.GetButtonValue(inputButtons[2]);
        var upHoldTime = _inputState.GetButtonHoldTime(inputButtons[1]);

        if (up && upHoldTime < 0.1f)
            _spring.distance -= 2.0f;

        if (down)
            _spring.distance += 2.0f;
    }

    void OnGrapplingHookDetach() // Uninitialze grappling hook
    {
        _spring.enabled = false;
        _attached = false;
        ToggleScripts(true);
    }
}