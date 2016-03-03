using UnityEngine;
using System.Collections;

public class POIAnchor : MonoBehaviour
{
    public Transform player;
    public Transform POI;

    public float weightPOI;
    public float weightPlayer;

    void FixedUpdate()
    {
        var pos = transform.position;

        pos = ((player.position * weightPlayer) + (POI.position * weightPOI)) / 2.0f;

        transform.position = pos;
    }

    void AddFocus(float val)
    {
        weightPOI += val;
        weightPlayer -= val;
    }
}
