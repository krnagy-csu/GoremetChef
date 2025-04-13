using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Script for the player smacking things around. 

    public float attackRange = 2f;
    public int damage = 10;

    //Every item the player can hit will go on a "Hittable" layer.
    public LayerMask hittableLayer;

    public Transform attackOrigin;

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
        //Cast a ray to see if there is a hittable object within the attack range of the player.
        RaycastHit hit;
        if (Physics.Raycast(attackOrigin.position, attackOrigin.forward, out hit, attackRange, hittableLayer))
        {
            //To see the attack range, debug purposes only
            Debug.DrawLine(attackOrigin.position, hit.point, Color.red, 1f);
            // Debug.Log("Hit: " + hit.collider.name);
            
            //Get the hittable object
            InteractableObject interact = hit.collider.GetComponent<InteractableObject>();

            //Call method to destroy the hittable object
            if (interact)
            {
                interact.TakeDamage();
            }
        }
    }
}
