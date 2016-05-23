using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour
{
    private Vector2 position = new Vector2(0, 0);

    private Vector2 resetPosition = new Vector2(0, 0);
    public Vector2 ResetPosition
    {
        get { return resetPosition; }
        set { return; }
    }

    public void SetLastPlayerPosition(Vector2 val)
    {
        position = val;
    }

    public Vector2 GetLastPlayerPosition()
    {
        return position;
    }
}
