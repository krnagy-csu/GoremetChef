using UnityEngine;

public class Character : MonoBehaviour
{
    //Simple character controller script for testing in interaction scene. -Rianne
    
    private CharacterController controller;
    [SerializeField] private float rotationSpeed = 10f;

    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move.magnitude > 0.1f) {
            controller.Move(move * (Time.deltaTime * speed));

            // rotates the player toward the movement direction
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
            // slerp makes the rotation smooth and we edit it with the rotation speed
        }
        
    }
}
