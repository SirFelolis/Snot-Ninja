using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    public Transform target;

    private CameraPosition _camPos;

    void Awake()
    {
        _camPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPosition>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _camPos.camState = CAMERA_STATE.FIXED;
            _camPos.pointOfInterest = (Vector2)target.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _camPos.camState = CAMERA_STATE.SMART;
            _camPos.pointOfInterest = new Vector2();
        }
    }

}
