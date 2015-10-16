using UnityEngine;
using System.Collections;

public class PlayerControlsAnchor : MonoBehaviour
{
    public GameObject player;
    public float smooth;
    public float verticalNegateFactor;

    void Update()
    {
        var pos = transform.position;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            pos.x = Mathf.Lerp(pos.x, player.transform.position.x + Input.GetAxis("Horizontal") * 80, smooth);
            pos.y = Mathf.Lerp(pos.y, player.transform.position.y + (Input.GetAxis("Vertical") * 80) / verticalNegateFactor, smooth);
        }
        transform.position = pos;
    }

}
