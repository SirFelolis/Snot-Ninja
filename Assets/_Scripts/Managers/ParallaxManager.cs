using UnityEngine;
using System.Collections;

public class ParallaxManager : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smooth;

    private Transform cam;
    private Vector3 previousCamPos;

    [SerializeField]
    private bool culling = true;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position; // gets updated every frame

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    void FixedUpdate()
    {

        for (int i = 0; i < backgrounds.Length; i++) 
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x + parallax, backgrounds[i].position.y, backgrounds[i].position.z);

            if (culling)
            {
                if (cam.position.x - backgrounds[i].position.x < 200.0f)
                {
                    backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smooth * Time.deltaTime);
                }
            }
            else
            {
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smooth * Time.deltaTime);
            }

        }

        previousCamPos = cam.position;
    }
}
