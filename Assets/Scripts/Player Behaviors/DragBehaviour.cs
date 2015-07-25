using UnityEngine;
using System.Collections;

public class DragBehaviour : AbstractBehavior
{
    public float dragInAir = 0.0f;

    private float defaultDrag;

    void Start()
    {
        defaultDrag = _rb2d.drag;
    }

    void Update()
    {
        if (!_collisionState.standing)
            _rb2d.drag = dragInAir;
        else
            _rb2d.drag = defaultDrag;
    }
}
