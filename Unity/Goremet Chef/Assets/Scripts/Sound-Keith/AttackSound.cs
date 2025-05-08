using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioClip attackClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        if (attackClip != null)
            audioSource.PlayOneShot(attackClip);
    }
}
