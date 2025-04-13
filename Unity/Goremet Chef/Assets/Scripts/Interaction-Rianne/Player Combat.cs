using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Script for the player smacking things around. 

    public float attackRange = 2f;
    public Transform attackOrigin;
    public float attackAngle = 90f; //Cone angle in degrees
    public int damage = 10;

    //Every item the player can hit will go on a "Hittable" layer.
    public LayerMask hittableLayer;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //To see player attack range outside of attacking
        Debug.DrawRay(attackOrigin.position, attackOrigin.forward * attackRange, Color.blue);
        
        //Player left click to attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackOrigin.position, attackRange, hittableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(attackOrigin.forward, directionToTarget);

            //Make sure the object is in front of the player and that they can't hit something behind them
            if (angle <= attackAngle / 2f)
            {
                Debug.Log("Hit: " + hitCollider.name);
                InteractableObject interact = hitCollider.GetComponent<InteractableObject>();
                
                //Check if the object exists
                if (interact)
                {
                    interact.TakeDamage();
                }
            }

            //Cast a ray to see if there is a hittable object within the attack range of the player.
        // RaycastHit hit;
        // if (Physics.Raycast(attackOrigin.position, attackOrigin.forward, out hit, attackRange, hittableLayer))
        // {
        //     //To see the attack range, debug purposes only
        //     Debug.DrawLine(attackOrigin.position, hit.point, Color.red, 1f);
        //     // Debug.Log("Hit: " + hit.collider.name);
        //     
        //     //Get the hittable object
        //     InteractableObject interact = hit.collider.GetComponent<InteractableObject>();
        //
        //     //Call method to destroy the hittable object
        //     if (interact)
        //     {
        //         interact.TakeDamage();
        //     }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackOrigin == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRange);
    }
}
