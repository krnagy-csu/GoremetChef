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
            playerSpotting.SetVisibility(playerSpotting.GetBaseVisiblity() / 2);
        }
        else 
        {
            isCrouched = false;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move.sqrMagnitude > 0.05f)
        {
            // rotates the player toward the movement direction
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            // slerp makes the rotation smooth and we edit it with the rotation speed
        }
        controller.Move(move * (Time.deltaTime * speed));

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

    public void SpeedBoost()
    {
        //STAMINA BOOST INSTEAD
        Debug.Log("SPEED BOOST ACTIVATED");
    }

    public void StealthBoost()
    {
        //THIS NEEDS TO BE CONNECTED TO PLAYERSPOTTING
        Debug.Log("STEALTH BOOST ACTIVATED");
    }
}