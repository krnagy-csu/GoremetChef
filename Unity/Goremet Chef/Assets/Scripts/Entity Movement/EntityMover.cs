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
    public bool wander = true;
    public float wanderRadius = 3.0f;
    public float minTimeToWander = 1.0f;
    public float maxTimeToWander = 10.0f;

    [Header("Editor-only Settings")]
    public Color sightRadiusColor;
    bool spooked;
    Vector3 startPos;

    public Transform player;
    private float timer = 0;
    private NavMeshAgent agent;

    private void OnDrawGizmos()
    {
        Gizmos.color = sightRadiusColor;
        Gizmos.DrawSphere(transform.position, sightRadius);
    }

    private void Start()
    {
        startPos = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (wander)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Wander();
            }
        }
    }

    /// <summary>
    /// When called, picks a random point within wanderRadius and sets it as the AI navigation destination.
    /// </summary>
    private void Wander() {
        float direction = Random.Range(0, 360);
        direction = Mathf.Deg2Rad * direction;
        float distance = Random.Range(0, wanderRadius);
        Vector3 finalPos = startPos + (new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction))) * distance;
        agent.SetDestination(finalPos);
        timer = Random.Range(minTimeToWander, maxTimeToWander);
    }
}
