using UnityEngine;
using System.Collections;

public class PlayerVelocityAnchor : MonoBehaviour
{
    public GameObject player;
    public float smooth;
    public float verticalNegateFactor;

    private Rigidbody2D _prb;

    void Start()
    {
        _prb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var pos = transform.position;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            pos.x = Mathf.Lerp(pos.x, player.transform.position.x + _prb.velocity.x, smooth);
            pos.y = Mathf.Lerp(pos.y, player.transform.position.y + _prb.velocity.y / verticalNegateFactor, smooth);
        }
        transform.position = pos;
    }
}
