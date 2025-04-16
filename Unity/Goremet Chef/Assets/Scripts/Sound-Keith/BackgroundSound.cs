using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip standardBackgroundMusic;
    public AudioClip hunterMusic;

    private AudioSource audioSource;
    private bool isPlayingHunterMusic = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayStandardMusic();
    }

    public void PlayHunterMusic()
    {
        if (hunterMusic != null && !isPlayingHunterMusic)
        {
            isPlayingHunterMusic = true;
            audioSource.clip = hunterMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayStandardMusic()
    {
        if (standardBackgroundMusic != null)
        {
            isPlayingHunterMusic = false;
            audioSource.clip = standardBackgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}

