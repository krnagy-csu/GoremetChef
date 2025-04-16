using UnityEngine;

public class ChopSound : MonoBehaviour
{
    public AudioClip chopClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void StartChopping()
    {
        if (chopClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = chopClip;
            audioSource.Play();
        }
    }

    public void StopChopping()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

