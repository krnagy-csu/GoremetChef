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
    public int timesToFlee = 2;
    public float secondsSpooked = 30f;
    public float fleeSpeed = 15f;

    [Header("Wander Settings")]
    public bool wander = true;
    public float wanderRadius = 3.0f;
    public float minTimeToWander = 1.0f;
    public float maxTimeToWander = 10.0f;
    public float wanderSpeed = 5f;

    [Header("Editor-only Settings")]
    public Color sightRadiusColor;
    public int maxLoops = 30;
    public bool doDebugLogging; //This should probably be moved to a static controller.

    //things we don't need the designer to see
    bool spooked;
    Vector3 startPos;
    public GameObject player;
    private float wanderTimer = 0;
    private float spookedTimer = 0;
    private NavMeshAgent agent;
    private GameObject[] hidingSpots;
    private GameObject targetSpot;
    private PlayerSpotting playSpot;

    private Animator animator;

    private void OnDrawGizmos()
    {
        Gizmos.color = sightRadiusColor;
        RaycastHit hit;
        if (player != null)
        {
            Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit);
            if (hit.transform.gameObject.CompareTag("Player"))
                {
                    Gizmos.color = Color.green;
                }
            else { Gizmos.color = Color.red; }
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    private void Start()
    {
        startPos = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        hidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
        playSpot = player.GetComponent<PlayerSpotting>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Walking", (agent.velocity != Vector3.zero));


        if (wander && !spooked)
        {
            wanderTimer -= Time.deltaTime;
            if (wanderTimer <= 0)
            {
                Wander();
                animator.SetFloat("Speed", 0.4f);
            }
        }

        if (spooked)
        {
            spookedTimer -= Time.deltaTime;
            if (spookedTimer <= 0)
            {
                spooked = false;
                targetSpot = null;
                if (doDebugLogging)
                {
                    Debug.Log("Calmed down, returning to wander");
                }
            }
        }
        
        float distToPlayerSqr = (player.transform.position - gameObject.transform.position).sqrMagnitude;
        if (distToPlayerSqr < (playSpot.GetVisibility() * playSpot.GetVisibility()) && targetSpot == null)
        {
            if (CheckVision())
            {
                Flee();
            }
        }
    }

    /// <summary>
    /// When called, picks a random point within wanderRadius and sets it as the AI navigation destination.
    /// </summary>
    private void Wander() {
        agent.speed = wanderSpeed;
        float direction = Random.Range(0, 360);
        direction = Mathf.Deg2Rad * direction;
        float distance = Random.Range(0, wanderRadius);
        Vector3 finalPos = startPos + (new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction))) * distance;
        agent.SetDestination(finalPos);
        wanderTimer = Random.Range(minTimeToWander, maxTimeToWander);
    }

    /// <summary>
    /// When called, picks a random hiding spot (first trying to find ones away from the player) and paths to it.
    /// </summary>
    private void Flee ()
    {
        agent.speed = fleeSpeed;
        spooked = true;
        spookedTimer = secondsSpooked;
        if (!PickHidingSpot())
        {
            targetSpot = hidingSpots[Random.Range(0, hidingSpots.Length)];
            if (doDebugLogging)
            {
                Debug.Log("Picked backup spot" + targetSpot.transform.position);
            }
        }
        agent.SetDestination(targetSpot.transform.position);

        /*
        while (!spotChosen)
        {
            if (loops < maxLoops){
                PickHidingSpot(loops);
                Vector3 dirToSpot = gameObject.transform.position - targetSpot.gameObject.transform.position;
                if (Vector3.Angle(dirToSpot,dirToPlayer) > 90)
                {
                    if (doDebugLogging) {
                        Debug.Log("Fleeing to " + targetSpot.transform.position);
                    }
                    agent.SetDestination(targetSpot.transform.position);
                    spotChosen = true;
                }
            } else
            {
                spotChosen = true;
                Debug.LogWarning("Failed to pick valid flee target, picking random spot instead");

                float direction = Random.Range(0, 360);
                direction = Mathf.Deg2Rad * direction;
                float distance = Random.Range(0, wanderRadius);
                Vector3 finalPos = startPos + (new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction))) * distance;
                agent.SetDestination(finalPos);
            }
        }
        animator.SetFloat("Speed", 2);
        */

    }

    /// <summary>
    /// Iterates through the list of hiding spots until it finds one away from the enemy
    /// (>90 degrees to the vector pointing to the enemy)
    /// </summary>
    private bool PickHidingSpot()
    {
        shuffleSpots(hidingSpots);
        for (int i = 0; i < hidingSpots.Length; i++)
        {
            Vector3 dirToPlayer = gameObject.transform.position - player.transform.position;
            GameObject tempSpot = hidingSpots[i];
            Vector3 dirToSpot = gameObject.transform.position - tempSpot.gameObject.transform.position;
            if (Vector3.Angle(dirToSpot,dirToPlayer) > 90)
            {
                if (doDebugLogging)
                {
                    Debug.Log("Fleeing to " + tempSpot.transform.position);
                    targetSpot = tempSpot;
                    return true;
                }
            }
        }
        return false;
    }
    
    private bool CheckVision()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit);
        return (hit.transform.gameObject.CompareTag("Player"));
    }

    GameObject[] shuffleSpots(GameObject[] spots)
    {
        for (int i = 0; i < spots.Length; i++)
        {
            GameObject temp = spots[i];
            int random = Random.Range(i, spots.Length);
            spots[i] = spots[random];
            spots[random] = temp;
        }
        return spots;
    }
}
