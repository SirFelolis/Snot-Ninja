using UnityEngine;

public class PlayerCenter : MonoBehaviour
{
    public Transform player;
    public float smooth = .2f;

    void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, player.position.x, smooth),
                Mathf.Lerp(transform.position.y, player.position.y, smooth),
                transform.position.z);

            var pos = transform.position;

            pos.x = (int)pos.x;
            pos.y = (int)pos.y;
        }
    }
}
