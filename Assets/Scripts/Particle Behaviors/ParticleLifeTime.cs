using UnityEngine;
using System.Collections;

public class ParticleLifeTime : MonoBehaviour
{

    void FixedUpdate()
    {
        Destroy(gameObject, 3.0f);
    }
}
