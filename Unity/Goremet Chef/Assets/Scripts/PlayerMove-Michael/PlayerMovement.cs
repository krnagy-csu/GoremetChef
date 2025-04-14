using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // character controller - Michael
    // Base simple character controller created by Rianne (given permission)
    
    private CharacterController controller;

    public float speed = 5f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * speed));
    }
}