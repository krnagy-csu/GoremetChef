using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // character controller - Michael
    // simple character controller created by Rianne used as base (given permission)
    
    private CharacterController controller;
    private bool isGrounded;

    [Header("Player Movement")]
    public float speed = 5f;
    public float jump = 5f;

    [Header("Stamina Gauges")]
    public float stamina = 50f;
    public float currentstam;
    private float gravity = -9.8f;
    private float groundGravity = -0.05f;
    private float verticalVelocity = 0f;
    private float stamDrain = 25f;
    private float stamRegen = 10f;
    private float RegenDelay = 4f;
    private float RegenTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentstam = stamina;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * speed));

        if (isGrounded) 
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
}