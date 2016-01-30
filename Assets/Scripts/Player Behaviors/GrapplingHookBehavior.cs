using UnityEngine;
using System.Collections;

/** Grappling hook script
*/

public class GrapplingHookBehavior : AbstractBehavior
{
    public LayerMask whatIsGround;

    public bool canGrapple;
    public bool hasShot;
    public bool shooting;
    public bool aiming;

    public float coolDown = 0.5f;
    private float time;

    public GameObject ball;
    public GameObject ballHit;
    public GameObject hookLine;

    [HideInInspector]
    public bool attached = false;
    [HideInInspector]
    public Vector2 attachedPoint = new Vector2();

    private Transform _nose;

    protected override void Awake()
    {
        base.Awake();

        _nose = GameObject.Find("Nose").transform;
    }

    void FixedUpdate()
    {
        if (time <= 0)
        {
            canGrapple = true;
        }
        else
        {
            time -= Time.deltaTime;
            canGrapple = false;
        }
    }

	void Update()
	{
        var shootHook = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (shootHook && holdTime < 0.01f && !attached && canGrapple && !_collisionState.standing && !hasShot)
        {
            OnShootHook();
            shooting = true;
            time = coolDown;
        }

        if (attached && shooting)
        {
            shooting = false;
            hasShot = true;
            OnGrapplingHookAttach();
        }

        if (attached && !_collisionState.standing && !_collisionState.onWall)
        {
            Attached();
        }

        if ((!shootHook || _collisionState.onWall || _collisionState.onCeil) && attached)
        {
            OnGrapplingHookDetach();
        }

        if (_collisionState.standing)
        {
            hasShot = false;
        }
    }

    void OnShootHook()
    {
        GameObject obj = (GameObject)Instantiate(ball, _nose.position, transform.rotation); // Ball
        float vert = new float(), horz = new float();
        horz = Input.GetAxis("HorizontalAlt");
        vert = Input.GetAxis("VerticalAlt");

        if (Mathf.Abs(horz) < 1)
        {
            horz = -(int)_inputState.direction;
        }

        obj.GetComponent<Rigidbody2D>().velocity = new Vector2((horz * 500), -vert * 500);
    }

    void OnGrapplingHookAttach() // Attach grappling hook
    {
        Instantiate(ballHit, attachedPoint, transform.rotation);
        Instantiate(hookLine,
            attachedPoint,
            Quaternion.AngleAxis(
                Mathf.Atan2(
                    (_nose.position - (Vector3)attachedPoint).y,
                    (_nose.position - (Vector3)attachedPoint).x) * Mathf.Rad2Deg,
                    Vector3.forward)); // Line

        attached = true;
        ToggleScripts(false);
    }

    void Attached()
    {
        _rb2d.velocity = ((Vector3)attachedPoint - transform.position).normalized * 250;
    }

    void OnGrapplingHookDetach() // Detach grappling hook
    {
        attached = false;
        ToggleScripts(true);
    }
}