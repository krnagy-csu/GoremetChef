using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    public AudioClip footstepClip;
    private AudioSource audioSource;
    private bool IsMoving = false;
    //This will definitely need to be changed, I imagine we'll just have one footstep sound that just loops until we're done walking
    //This can be implemented easily once we have the player controller set up

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            IsMoving = true;
            if (!audioSource.isPlaying)
            {
                StartWalking();
            }
        }
        else
        {
            IsMoving = false;
            StopWalking();
        }
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

