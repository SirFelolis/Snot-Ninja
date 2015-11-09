﻿using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    public Transform target;
    public float size = 100.0f;
    [Range(0.0f, 1.0f)]
    public float zoomSmooth = 0.2f;

    private float _defaultSize;
    private CameraPosition _camPos;

    void Awake()
    {
        _camPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPosition>();
        _defaultSize = _camPos.GetComponent<Camera>().orthographicSize;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _camPos.GetComponent<Camera>().orthographicSize = Mathf.Lerp(_camPos.GetComponent<Camera>().orthographicSize, size, zoomSmooth);
            _camPos.camState = CAMERA_STATE.SEMIFIXED;
            _camPos.pointOfInterest = (Vector2)target.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _camPos.GetComponent<Camera>().orthographicSize = _defaultSize;
            _camPos.camState = CAMERA_STATE.SMART;
            _camPos.pointOfInterest = new Vector2();
        }
    }

}
