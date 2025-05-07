using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Debug")]
    public bool doDebugLog; //Krisztian: adding debug.log statements to see why it's not recognizing entities as hittable, making them toggleable

    //Script for the player smacking things around. 

    public float attackRange = 2f;
    public Transform attackOrigin;
    public float attackAngle = 90f; //Cone of attack angle in degrees
    
    //For if different objects have different health. I didn't add this yet for simplicity.
    public int damage = 1;
    
    //Strength boost variables
    public bool isStrengthBoosted;
    public int boostedDamage = 2;
    public float strengthBoostDuration = 8f;

    //Every item the player can hit will go on a "Hittable" layer.
    public LayerMask hittableLayer;
    public LayerMask pickableLayer;

    // Update is called once per frame
    void Update()
    {
        
        //Player left click to attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
             PickUp();
        }
    }

    void Attack()
    {
        //Calls overlapshere to see what is in the player attack range and stores all the colliders it hits in an array
        Collider[] hitColliders = Physics.OverlapSphere(attackOrigin.position, attackRange, hittableLayer);
        Debug.Log(hitColliders.ToString());
        foreach (Collider hitCollider in hitColliders)
        {
            //Krisztian: debug.log statement to see what exactly we hit
            Debug.Log(hitCollider.gameObject.name);
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
                    interact.TakeDamage(damage);
                    GetComponent<AttackSound>()?.PlayAttackSound();
                }
            }
        }
    }

    void PickUp()
    {
        Debug.Log("PickUp called");
        Collider[] hitColliders = Physics.OverlapSphere(attackOrigin.position, attackRange, pickableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("Checking hits");
            //Shows which direction the target is in relation to player, then calculates the angle between the direction and the front of the player.
            //This is to check the attack angle and make sure the object isn't behind player
            Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(attackOrigin.forward, directionToTarget);
            
            //Checks if object is in front of the player and that they can't hit something behind them
            if (angle <= attackAngle / 2f)
            {
                Debug.Log("Picked up: " + hitCollider.name);
                Item item = hitCollider.GetComponent<ItemController>().item;
                
                //This is where I'm gonna have to check weight I think. 
                int currentWeight = InventoryManager.Instance.GetCurrentWeight();
                int newWeight = currentWeight + item.weight;
                int weightLimit = InventoryManager.Instance.GetWeightLimit();
                
                if (newWeight > weightLimit)
                {
                    Debug.Log("THAT SHIT'S TOO HEAVY.");
                }
                else
                {
                   InventoryManager.Instance.Add(item);
                   Destroy(hitCollider.gameObject); 
                }
                
                

                /*
                Krisztian's cursed way of doing it that requires exposing the PickUp method in the pickUpScript
                GameObject other = hitCollider.gameObject;
                ItemPickUp pickUpScript = other.GetComponent<ItemPickUp>();
                if (pickUpScript != null)
                {
                    Debug.Log("Pickup firing");
                    pickUpScript.PickUp();
                }*/
            }
        }
    }
    
    public void StrengthBoost()
    {
        Debug.Log("STRENGTH BOOST ACTIVATED");
        if (!isStrengthBoosted)
        {
            StartCoroutine(StrengthBoostCoroutine());
        }
    }

    private IEnumerator StrengthBoostCoroutine()
    {
        isStrengthBoosted = true;
        int originalDamage = damage;
        damage = boostedDamage;
        
        yield return new WaitForSeconds(strengthBoostDuration);
        
        damage = originalDamage;
        isStrengthBoosted = false;
        Debug.Log("isStrengthBoosted: false");
    }
    

    //Debug only. See the overlapshere of the attack range.
    void OnDrawGizmos()
    {
        if (attackOrigin == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRange);
    }
}
