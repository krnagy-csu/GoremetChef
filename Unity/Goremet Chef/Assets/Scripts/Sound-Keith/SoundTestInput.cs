using UnityEngine;

public class SoundTestInput : MonoBehaviour
{
    [Header("Sound Source References")]
    public BackgroundSound backgroundSound;
    public CookingSizzleSound cookingSizzle;
    public ChopSound chop;
    public FootStepSound footsteps;
    public ItemPickupSound itemPickup;
    public ZombieSounds zombieSounds;

    private bool isChopping = false;
    private bool isSizzling = false;
    private bool isWalking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            backgroundSound?.PlayStandardMusic();
            Debug.Log("Played: Standard Background Music");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            backgroundSound?.PlayHunterMusic();
            Debug.Log("Played: Hunter Music");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isChopping = !isChopping;
            if (isChopping)
            {
                chop?.StartChopping();
                Debug.Log("Started: Chopping Sound");
            }
            else
            {
                chop?.StopChopping();
                Debug.Log("Stopped: Chopping Sound");
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isSizzling = !isSizzling;
            if (isSizzling)
            {
                cookingSizzle?.StartSizzle();
                Debug.Log("Started: Cooking Sizzle");
            }
            else
            {
                cookingSizzle?.StopSizzle();
                Debug.Log("Stopped: Cooking Sizzle");
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            itemPickup?.PlayPickupSound();
            Debug.Log("Played: Item Pickup");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isWalking = !isWalking;
            if (isWalking)
            {
                footsteps?.StartWalking();
                Debug.Log("Started: Footsteps");
            }
            else
            {
                footsteps?.StopWalking();
                Debug.Log("Stopped: Footsteps");
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            zombieSounds?.PlayHappyZombieSound();
            Debug.Log("Played: Happy Zombie Sound");
        }
    }
}

