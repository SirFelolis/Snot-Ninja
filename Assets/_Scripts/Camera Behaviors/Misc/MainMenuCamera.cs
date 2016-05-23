using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions;

    [SerializeField] // Serialized for testing purposes
    private Transform targetPosition;

    [SerializeField]
    private string targetName;
    public string TargetName
    {
        get { return targetName; }
        set { targetName = value; }
    }

    [SerializeField]
    private float smooth;

    void FixedUpdate ()
    {
        switch (targetName)
        {
            case "Main":
                targetPosition = positions[0];
                break;
            case "Settings":
                targetPosition = positions[1];
                break;
            default:
                targetPosition = null;
                break;
        }

        if (targetPosition != null)
        {
            var pos = transform.position;

            pos.x = Mathf.SmoothStep(pos.x, targetPosition.position.x, smooth);
            pos.y = Mathf.SmoothStep(pos.y, targetPosition.position.y, smooth);

            transform.position = pos;
        }
    }
}
