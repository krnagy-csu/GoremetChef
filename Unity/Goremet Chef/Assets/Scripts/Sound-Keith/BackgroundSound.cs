using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip standardBackgroundMusic;
    public AudioClip hunterMusic;

    private AudioSource audioSource;
    private bool isPlayingHunterMusic = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Town" && hunterMusic != null)
        {
            PlayHunterMusic();
        }
        else if (sceneName == "Kitchen" && standardBackgroundMusic != null)
        {
            PlayStandardMusic();
        }

        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayHunterMusic()
    {

        isPlayingHunterMusic = true;
        audioSource.clip = hunterMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayStandardMusic()
    {
        isPlayingHunterMusic = false;
        audioSource.clip = standardBackgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}

