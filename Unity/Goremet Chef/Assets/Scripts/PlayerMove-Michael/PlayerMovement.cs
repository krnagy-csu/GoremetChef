using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // character controller - Michael
    // simple character controller created by Rianne used as base (given permission)
    
    private CharacterController controller;
    private PlayerSpotting playerSpotting;
    private bool isGrounded;
    private bool isCrouched = false;

    [Header("Player Movement")]
    public float speed = 5f;
    public float jump = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Stamina Gauges")]
    public float stamina = 50f;
    public float currentstam;
    private float stamDrain = 25f;
    private float stamRegen = 10f;
    private float RegenDelay = 4f;
    private float RegenTimer = 0f;

    // Gravity variables
    private float gravity = -9.8f;
    private float groundGravity = -0.05f;
    private float verticalVelocity = 0f;

    // Crouch variables
    public float standHeight = 2f;
    public float crouchHeight = 1f;
    
    // Animation variables
    [SerializeField] private Animator playerAnimator;
    //Stealth boost variables
    public bool isStealthBoosted;
    public float stealthDuration = 8f;
    
    //Stamina boost variables
    public bool isStaminaBoosted;
    public float boostedMaxStamina = 100f;
    public float staminaBoostDuration = 8f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerSpotting = GetComponent<PlayerSpotting>();
        currentstam = stamina;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        playerSpotting.SetVisibility(playerSpotting.GetBaseVisiblity());
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !isCrouched)
        {
            if (currentstam > 0f)
            {
                speed = 10f;
                currentstam -= stamDrain * Time.deltaTime;
                currentstam = Mathf.Clamp(currentstam, 0f, stamina);
                RegenTimer = 0f;
                if (currentstam == 0f)
                {
                    speed = 5f;
                }
                else
                {
                    playerSpotting.SetVisibility(playerSpotting.GetBaseVisiblity() * 1.5f);
                }
            }
        }
        else
        {
            speed = 5f;
            if (RegenTimer >= RegenDelay && currentstam != stamina) 
            {
                currentstam += stamRegen * Time.deltaTime;
                currentstam = Mathf.Clamp(currentstam, 0f, stamina);
            }
            else
            {
                RegenTimer += Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Q) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) 
        {
            isCrouched = true;
            speed = speed / 5;

            //Check if the player has the stealth boost active to make range smaller
            if (isStealthBoosted)
            {
                playerSpotting.SetVisibility(playerSpotting.GetBaseVisiblity() / 2.5f);
                //This makes the range 3.2. Want it smaller? Bigger?
            }
            else //Otherwise, make it the normal crouch range
            {
                playerSpotting.SetVisibility(playerSpotting.GetBaseVisiblity() / 2);
            }
            
        }
        else 
        {
            isCrouched = false;
        }
        
        //Set movement animation to false so that it stops the frame inputs stop
        playerAnimator.SetBool("Moving",false);
        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float walkInput = move.sqrMagnitude;
        if (walkInput > 0.01f)
        {
            //Set movement animation to true so it moves
            playerAnimator.SetBool("Moving", true);
            // rotates the player toward the movement direction
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            // slerp makes the rotation smooth and we edit it with the rotation speed
        }
        controller.Move(move * (Time.deltaTime * speed)); 
        playerAnimator.SetFloat("Speed", Mathf.Abs(0.5f + (speed / 10f)));

        if (isGrounded && !isCrouched) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jump;
            }
            else
            {
                verticalVelocity = groundGravity;
            }
        } 
        else 
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        
        move.y = verticalVelocity;
        controller.Move(move * (jump * Time.deltaTime));
    }

    public void StaminaBoost()
    {
        Debug.Log("STAMINA BOOST ACTIVATED");
        if (!isStaminaBoosted)
        {
            StartCoroutine(StaminaBoostCoroutine());
        }
    }

    private IEnumerator StaminaBoostCoroutine()
    {
        isStaminaBoosted = true;
        
        //Save the original max stamina
        float originalMaxStamina = stamina;
        stamina = boostedMaxStamina;
        //Fill the player's current stamina to the new maximum 
        currentstam = stamina;
        
        yield return new WaitForSeconds(staminaBoostDuration);
        
        stamina = originalMaxStamina;
        //Clamp current stam so if it's above the og max stamina, it won't stay above it when reverted.
        currentstam = Mathf.Min(currentstam, stamina);
        isStaminaBoosted = false;
    }

    public void StealthBoost()
    {
        Debug.Log("STEALTH BOOST ACTIVATED");
        if (!isStealthBoosted)
        {
            StartCoroutine(StealthActivated());
        }
        
    }

    private IEnumerator StealthActivated()
    {
        //Right now this is ONLY affecting when crouched bc I'd have to do some fenagling and want to see how we feel abt it.
        isStealthBoosted = true;
        Debug.Log("isStealthBoosted: true");
        float originalRange = playerSpotting.GetBaseVisiblity();
        
        
        yield return new WaitForSeconds(stealthDuration);
        
        isStealthBoosted = false;
        Debug.Log("isStealthBoosted: false");
        
    }
}