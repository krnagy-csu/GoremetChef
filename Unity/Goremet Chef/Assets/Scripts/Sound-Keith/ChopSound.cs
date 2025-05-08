using UnityEngine;

public class ChopSound : MonoBehaviour
{
    public AudioClip chopClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlayChopSound()
    {
        if (chopClip != null)
        {
            audioSource.PlayOneShot(chopClip);
        }
    }
    
//Disreguard these
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

