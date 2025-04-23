using System.Collections;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    public AudioClip[] randomZombieSounds;      // I made it an array so if we want to change the number we can but I'm assuming we'll have like 5
    public AudioClip happyZombieSound;          // This is for when we complete an order

    private AudioSource audioSource;
    private Coroutine randomSoundCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomSoundCoroutine = StartCoroutine(PlayRandomZombieSounds());
    }

    IEnumerator PlayRandomZombieSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);

            if (!audioSource.isPlaying && randomZombieSounds.Length > 0)
            {
                AudioClip randomClip = randomZombieSounds[Random.Range(0, randomZombieSounds.Length)];
                audioSource.PlayOneShot(randomClip);
            }
        }
    }

    public void PlayHappyZombieSound()
    {
        if (happyZombieSound != null)
        {
            audioSource.PlayOneShot(happyZombieSound);
        }
    }
}

