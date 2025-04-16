using UnityEngine;

public class ItemPickupSound : MonoBehaviour
{
    public AudioClip pickupClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        if (pickupClip != null)
            audioSource.PlayOneShot(pickupClip);
    }
}

