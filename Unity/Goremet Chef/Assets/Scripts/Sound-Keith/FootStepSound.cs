using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    public AudioClip footstepClip;
    private AudioSource audioSource;
    //This will definitely need to be changed, I imagine we'll just have one footstep sound that just loops until we're done walking
    //This can be implemented easily once we have the player controller set up

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void StartWalking()
    {
        if (footstepClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = footstepClip;
            audioSource.Play();
        }
    }

    public void StopWalking()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

