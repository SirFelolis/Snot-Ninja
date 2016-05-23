using UnityEngine;
using System.Collections;

public class ControllerStickAnchor : MonoBehaviour
{
    Transform playerPos;

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

	
	// Update is called once per frame
	void Update ()
	{
        var pos = transform.position;

        if (playerPos != null)
            pos = playerPos.position;

        pos.x += Input.GetAxisRaw("HorizontalRightStick") * 200;
        pos.y -= Input.GetAxisRaw("VerticalRightStick") * 200;

        transform.position = pos;
	}
}
