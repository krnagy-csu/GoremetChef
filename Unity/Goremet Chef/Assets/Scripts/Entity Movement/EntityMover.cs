using UnityEngine;
using UnityEngine.AI;
public class EntityMover : MonoBehaviour
{
    /*
     * Krisztian Nagy
     * Simple movement for 'entities' (i.e. civilians.)
     * When in a normal state, they'll just wander a short distance around their starting position.
     * When spooked, they'll pick a random hiding spot and path to it.
     * If the hiding spot requires running past the thing that spooked them, they'll instead pick a new one. 
     * For now, 'running past' is determined by simple direction to the thing that spooked them.
     * This has an obvious weakness 
     */
    [Header("Spook Settings")]
    public float sightRadius = 3.0f;
    public int timesToFlee = 2;

    [Header("Wander Settings")]
    public float wanderRadius = 3.0f;
    public float minTimeToWander = 1.0f;
    public float maxTimeToWander = 10.0f;

    [Header("Editor-only Settings")]
    public Color sightRadiusColor;
    bool spooked;
    Vector3 startPos;

    private void OnDrawGizmos()
    {
        Gizmos.color = sightRadiusColor;
        Gizmos.DrawSphere(transform.position, sightRadius);
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        
    }
}
