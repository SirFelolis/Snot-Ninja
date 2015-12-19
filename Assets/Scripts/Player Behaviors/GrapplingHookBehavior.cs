using UnityEngine;
using System.Collections;

/** Grappling hook script
*/

public class GrapplingHookBehavior : AbstractBehavior
{
    public LayerMask whatIsGround;

    public bool isGrappled;

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
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, new Vector2(45.0f * -(float)_inputState.direction, 45.0f),
            Mathf.Infinity, whatIsGround); // TODO: Should not have infinite reach
        Vector3 grapplePoint = hit.point;

        grapplePoint = hit.point;
        return grapplePoint;
    }

    void OnGrapplingHookAttach() // Initialize grappling hook
    {
        isGrappled = true;
        _spring.connectedAnchor = SetGrapplingPoint();
        _spring.distance = (transform.position - (Vector3)_spring.connectedAnchor).magnitude;
        _spring.enabled = true;
        _attached = true;
        ToggleScripts(false);
    }

    void OnGrapplingHookAttached() // Run every frame the grappling hook is used
    {
        var value = 250;

        _rb2d.velocity = new Vector2(Mathf.Clamp(_rb2d.velocity.x, -value, value), Mathf.Clamp(_rb2d.velocity.y, -value, value));
    }

    void OnGrapplingHookDetach() // Uninitialze grappling hook
    {
        isGrappled = false;
        _spring.enabled = false;
        _attached = false;
        ToggleScripts(true);
    }
}