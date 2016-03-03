using UnityEngine;
using System.Collections;

/** Grappling hook script
*/

public class GrapplingHookBehavior : AbstractBehavior
{
    public LayerMask whatIsGround;

    public bool canGrapple;
    public bool shooting;
    public bool aiming;
    public float ballSpeed;

    public float timesShot = 0;
    public float hookLimit = 2;

    public float coolDown = 0.5f;
    private float time;

    public GameObject ball;
    public GameObject ballHit;
    public GameObject hookLine;

    private Jump _jumpScript;

    [HideInInspector]
    public bool attached = false;
    [HideInInspector]
    public Vector2 attachedPoint = new Vector2();

    private Transform _nose;

    protected override void Awake()
    {
        base.Awake();

        _jumpScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>();
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
//        var shootHook = _inputState.GetButtonValue(inputButtons[0]);
        var shootHook = Input.GetMouseButtonDown(1);
//        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (shootHook && !attached && canGrapple && !_collisionState.standing && timesShot < hookLimit)
        {
            OnShootHook();
            shooting = true;
            time = coolDown;
        }

        if (attached && shooting)
        {
            timesShot++;
            shooting = false;
            OnGrapplingHookAttach();
        }

        if (attached && !_collisionState.standing && !_collisionState.onWall)
        {
            Attached();
        }

        if ((!Input.GetMouseButton(1) || _collisionState.onWall || _collisionState.onCeil) && attached)
        {
            OnGrapplingHookDetach();
        }

        if (_collisionState.standing)
        {
            timesShot = 0;
            OnGrapplingHookDetach();
        }
    }

    void OnShootHook()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (worldMousePosition - transform.position).normalized;

        GameObject obj = (GameObject)Instantiate(ball, _nose.position, transform.rotation); // Ball
        obj.GetComponent<Rigidbody2D>().velocity = direction * ballSpeed;
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
        _rb2d.velocity = ((Vector3)attachedPoint - transform.position).normalized * 300;
    }

    void OnGrapplingHookDetach() // Detach grappling hook
    {
        if (_jumpScript.jumpsRemaining < 1)
            _jumpScript.jumpsRemaining++;
        attached = false;
        ToggleScripts(true);
    }
}