using UnityEngine;
using System.Collections;

/** Grenade explosion script
 * Makes the grenade explode.
*/

public class GrenadeExplode : MonoBehaviour
{
    public float cookTime;
    public GameObject smokeParticle;

    private float currentTime;
    private Rigidbody2D _rb2d;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= cookTime)
        {
            OnExplosion();
        }

    }

    void OnExplosion()
    {
        Instantiate(smokeParticle, 
            new Vector3(
            Random.Range(transform.position.x - 10, transform.position.x + 10), 
            Random.Range(transform.position.y - 5, transform.position.y + 10), 
            transform.position.z), 
            Quaternion.identity);
        Destroy(gameObject, 2.0f);
    }
}
