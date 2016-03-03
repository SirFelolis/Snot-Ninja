using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    public GameObject dissolveQuad;
    public Texture2D startDissolveMap;
    public Texture2D endDissolveMap;
    public float fadeSpeed = 1.5f;

    private bool sceneStarting = true;
    private Renderer rend;

    void Awake()
    {
        rend = dissolveQuad.GetComponent<Renderer>();
    }

    void Start()
    {
        rend.material.SetFloat("_SliceAmount", 0);
    }

    void FixedUpdate()
    {
        if (sceneStarting)
            StartScene();
    }

    void FadeToClear()
    {
        rend.material.SetFloat("_SliceAmount", Mathf.Lerp(rend.material.GetFloat("_SliceAmount"), 1, fadeSpeed * Time.deltaTime));
    }

    void FadeToBlack()
    {
        rend.material.SetFloat("_SliceAmount", Mathf.Lerp(rend.material.GetFloat("_SliceAmount"), 0, fadeSpeed * Time.deltaTime));
    }

    public void StartScene()
    {
        rend.material.SetTexture("_SliceGuide", startDissolveMap);

        FadeToClear();

        if (rend.material.GetFloat("_SliceAmount") >= 0.95f)
        {
            rend.material.SetFloat("_SliceAmount", 1);
            dissolveQuad.SetActive(false);

            sceneStarting = false;
        }
    }

    public void EndScene(int level)
    {
        dissolveQuad.SetActive(true);

        rend.material.SetTexture("_SliceGuide", endDissolveMap);

        if (!sceneStarting) FadeToBlack();

        if (rend.material.GetFloat("_SliceAmount") <= 0.05f)
        {
            SceneManager.LoadScene(level);
        }
    }

    public void EndScene(string level)
    {
        dissolveQuad.SetActive(true);

        rend.material.SetTexture("_SliceGuide", endDissolveMap);

        if (!sceneStarting) FadeToBlack();

        if (rend.material.GetFloat("_SliceAmount") <= 0.05f)
        {
            SceneManager.LoadScene(level);
        }
    }

}
