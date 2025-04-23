using UnityEngine;

public class basicCameraFollower : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = objectToFollow.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + offset;
    }
}
