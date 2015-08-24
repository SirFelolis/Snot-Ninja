using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
    public Vector2[] positions;

    private Vector2 totalPositions;
    private Vector2 averagePosition;
    void FixedUpdate()
    {
        foreach (var pos in positions)
            totalPositions += pos;

        if (positions.Length > 0)
        {
            averagePosition = totalPositions / positions.Length;
            transform.position = new Vector3(averagePosition.x, averagePosition.y, transform.position.z);
        }
    }

}
