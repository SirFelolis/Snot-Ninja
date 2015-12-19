using UnityEngine;
using System.Collections;

public enum CAMERA_STATE
{
    FIXED,
    SMART,
    SEMIFIXED,
}

public class CameraPosition : MonoBehaviour
{
    public Transform playerPos;
    public Transform playerVelAnchor;
    public Transform playerConAnchor;
    public GameObject playerObject;
    [Range(0.0f, 1.0f)]
    public float smooth;
    [Range(0.0f, 1.0f)]
    public float zoomSmooth;


    [HideInInspector]
    public CAMERA_STATE camState = CAMERA_STATE.SMART;

    [HideInInspector]
    public Vector2 pointOfInterest = new Vector2();

    private float startSmooth;

    [HideInInspector]
    public float orthoSize = 100.0f;

    private Vector2 cameraPosition = new Vector2();

    void Awake()
    {
        startSmooth = smooth;
    }

    void FixedUpdate()
    {
        if (smooth != startSmooth)
            smooth = startSmooth;
            
        if (GameObject.Find("Player") != null)
        {
            if (camState == CAMERA_STATE.SMART)
            {
                Vector2 averagePos = (playerPos.position + playerVelAnchor.position + playerConAnchor.position) / 3;
                cameraPosition = averagePos;
            }
            if (camState == CAMERA_STATE.FIXED)
            {
                smooth /= 1.5f;
                cameraPosition = pointOfInterest;
            }
            if (camState == CAMERA_STATE.SEMIFIXED)
            {
                smooth /= 2f;
                Vector2 averagePos = (playerPos.position * 0.1f + (Vector3)pointOfInterest * 1.9f) / 2;
                cameraPosition = averagePos;
            }
        }

        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(
            gameObject.GetComponent<Camera>().orthographicSize,
            orthoSize,
            zoomSmooth);

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, cameraPosition.x, smooth),
            Mathf.Lerp(transform.position.y, cameraPosition.y, smooth),
            transform.position.z);
    }
}
