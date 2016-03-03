using UnityEngine;
using System.Collections;

public class TextPrintSoundBehavior : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlayTypingSound(AudioClip typingSounds)
    {
        audioSource.PlayOneShot(typingSounds);
        yield return null;
    }
}
