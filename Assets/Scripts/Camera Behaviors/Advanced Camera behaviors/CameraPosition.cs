using UnityEngine;
using System.Collections;

public enum CAMERA_STATE
{
    FIXED,
    SMART,
}

public class CameraPosition : MonoBehaviour
{
    public Transform playerPos;
    public Transform playerVelAnchor;
    public Transform playerConAnchor;
    public GameObject playerObject;
    public float smooth;

    [HideInInspector]
    public CAMERA_STATE camState = CAMERA_STATE.SMART;
    
    [HideInInspector]
    public Vector2 pointOfInterest = new Vector2();

    private float startSmooth;

    private Vector2 cameraPosition = new Vector2();

    void Awake()
    {
        startSmooth = smooth;
    }

    void FixedUpdate()
    {
        if (smooth != startSmooth)
            smooth = startSmooth;
            

        if (camState == CAMERA_STATE.SMART)
        {
            Vector2 averagePos = (playerPos.position + playerVelAnchor.position + playerConAnchor.position) / 3;
/*            if (_playerRb2d.velocity.y != 0)
                cameraPosition.x = averagePos.x;
            else*/
                cameraPosition = averagePos;
        }
        if (camState == CAMERA_STATE.FIXED)
        {
            smooth /= 1.5f;
            cameraPosition = pointOfInterest;
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, cameraPosition.x, smooth), Mathf.Lerp(transform.position.y, cameraPosition.y, smooth), transform.position.z);
    }
}
