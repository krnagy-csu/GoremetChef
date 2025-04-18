using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Script for the player smacking things around. 

    public float attackRange = 2f;
    public Transform attackOrigin;
    public float attackAngle = 90f; //Cone of attack angle in degrees
    
    //For if different objects have different health. I didn't add this yet for simplicity.
    public int damage = 10;

    //Every item the player can hit will go on a "Hittable" layer.
    public LayerMask hittableLayer;

    // Update is called once per frame
    void Update()
    {
        
        //Player left click to attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Calls overlapshere to see what is in the player attack range and stores all the colliders it hits in an array
        Collider[] hitColliders = Physics.OverlapSphere(attackOrigin.position, attackRange, hittableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            //Shows which direction the target is in relation to player, then calculates the angle between the direction and the front of the player.
            //This is to check the attack angle and make sure the object isn't behind player
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(attackOrigin.forward, directionToTarget);

            //Checks if object is in front of the player and that they can't hit something behind them
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
        }
    }

    //Debug only. See the overlapshere of the attack range.
    void OnDrawGizmos()
    {
        if (attackOrigin == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRange);
    }
}
