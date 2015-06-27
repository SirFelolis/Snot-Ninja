using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Buttons[] input;

    private Rigidbody2D _rb2d;
    private InputState _inputState;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _inputState = GetComponent<InputState>();
    }

    void Update()
    {
        var right = _inputState.GetButtonValue(input[0]);
        var left = _inputState.GetButtonValue(input[1]);
        var velX = speed;

        if(right || left)
        {
            velX *= left ? -1 : 1;
        }
        else { velX = 0; }

        _rb2d.velocity = new Vector2(velX, _rb2d.velocity.y);
    }
}
