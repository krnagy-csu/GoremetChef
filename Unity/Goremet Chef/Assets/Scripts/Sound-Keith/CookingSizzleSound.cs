using UnityEngine;

public class CookingSizzleSound : MonoBehaviour
{
    public AudioClip sizzleClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // sizzle should loop while food is cooking but I was thinking maybe when we get further away from the food it quiets down, we can dedice or implement that later
    }

    public void StartSizzle()
    {
        if (sizzleClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = sizzleClip;
            audioSource.Play();
        }
    }

    public void StopSizzle()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

