using UnityEngine;

public class Character : MonoBehaviour
{
    //Simple character controller script for testing in interaction scene. -Rianne
    
    private CharacterController controller;

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
        controller.Move(move * (Time.deltaTime * speed));
    }
}
