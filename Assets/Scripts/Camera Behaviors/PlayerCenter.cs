using UnityEngine;

public class PlayerCenter : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.2f;

    void Update()
    {
        if (GameObject.Find("Player") != null) //TODO: make it compatible with objects other than the player
        {
            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, player.position.x, smooth),
                Mathf.Lerp(transform.position.y, player.position.y, smooth),
                transform.position.z);
        }

        var pos = transform.position;

        pos.x = (int)pos.x; // This gives the camera that NES feel when moving
        pos.y = (int)pos.y;

        transform.position = pos;

    }
}
