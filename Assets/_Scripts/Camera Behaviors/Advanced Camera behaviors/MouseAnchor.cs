using UnityEngine;
using System.Collections;

public class MouseAnchor : MonoBehaviour
{
    void FixedUpdate()
    {
        var pos = transform.position;
        pos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        pos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        transform.position = pos;

    }
}
