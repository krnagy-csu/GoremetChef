using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // character controller - Michael
    // simple character controller created by Rianne used as base (given permission)
    
    private CharacterController controller;
    private bool isGrounded;
    
    public float speed = 5f;
    public float jump = 5f;
    private float gravity = -9.8f;
    private float groundGravity = -0.05f;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

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