using UnityEngine;
using System.Collections;

public class GrapplingHookBallBehavior : MonoBehaviour
{
    private GrapplingHookBehavior _hook;

    void Awake()
    {
        _hook = GameObject.FindGameObjectWithTag("Player").GetComponent<GrapplingHookBehavior>();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Collision Object")
        {
            _hook.attachedPoint = transform.position;
            _hook.attached = true;
            Destroy(gameObject);
        }
    }

}
