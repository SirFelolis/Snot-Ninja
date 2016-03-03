using UnityEngine;
using System.Collections;

public class ParticleLifeTime : MonoBehaviour
{
    public bool mustFade;
    public float time;
    public float fadeSpeed;
    public float delay;

    void Start()
    {
        Destroy(gameObject, (time + delay) * 2);
        if (!mustFade)
            Destroy(gameObject, time + delay);
    }

    void FixedUpdate()
    {
        if (delay > 0) delay -= Time.deltaTime;

        if (mustFade && delay <= 0)
        {
            float fade = Mathf.SmoothDamp(GetComponent<SpriteRenderer>().color.a, 0, ref fadeSpeed, time);
            if (GetComponent<SpriteRenderer>().color.a != 0) GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fade);
        }
    }
}
