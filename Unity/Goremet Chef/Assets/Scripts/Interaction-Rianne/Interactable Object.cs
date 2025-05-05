using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //This is the script that should be on any item a player can hit. Garbage cans, dumpsters, civilians.
    //Interactable objects can take damage from player and drop loot.

    //This array holds a list of possible item drops which can be updated through the inspector.
    public GameObject[] possibleDrops;
    //This is where the item is gonna spawn.
    public Transform dropOrigin;
    
    //These are for loot llama only
    public bool isLootLlama = false;
    public int health = 2;
    public int maxLlamaHits = 3;
    private int currentHits;

    private GameObject[] myDrops;

    //ONLY USE IF WE WANT A SPECIFIC ITEM SPAWN EVERY TIME. Accompanying code is still included in the Die() method.
    // public GameObject dropPrefab;

    //This is when the object is hit by the player.

    private void Update()
    {
        myDrops = possibleDrops;
    }

    public void TakeDamage(int damage)
    {
        //Hit! 
        Debug.Log("Ouch! I'm " + gameObject.name);

        if (isLootLlama)
        {
            LootLlamaDamage();
        }
        else
        {
            Debug.Log("Damage: " + damage);
            health -= damage;
            Debug.Log("Health: " + health);
            if (health <= 0)
            {
                //Killed!
                Invoke(nameof(Die), 1f);
            }
        }
        
        
    }

    private void LootLlamaDamage()
    {
        currentHits++;
        
        //Drop 1 item for the hit
        if (myDrops.Length > 0)
        {
            int index = Random.Range(0, myDrops.Length);
            GameObject selectedDrop = myDrops[index];
            Vector3 spawnPos = dropOrigin != null ? dropOrigin.position : transform.position;
            Instantiate(selectedDrop, spawnPos, Quaternion.identity);
            Debug.Log("Dropped " + selectedDrop.name);
        }
        
        if (currentHits >= maxLlamaHits)
        {
            Die();
        }
    }

    //
    private void Die()
    {
        Debug.Log(gameObject.name + " destroyed.");
        
        if (!isLootLlama && myDrops.Length > 0)
        {
            //This will select an item drop randomly from an array of possible drop prefabs.
            int index = Random.Range(0, myDrops.Length);
            GameObject selectedDrop = myDrops[index];
            
            //This is to check if the drop origin for the item exists, and on the off-chance it doesn't, it'll just spawn at the object's origin.
            Vector3 spawnPosition = dropOrigin.position != null ? dropOrigin.position : transform.position;
            
            //I grant you existence, item drop. Use it wisely...
            Instantiate(selectedDrop, spawnPosition, Quaternion.identity);
        } 
        
        //--------THIS CODE CAN BE USED IF WE WANT TO SPAWN A SPECIFIC ITEM DROP EVERY TIME. But I thought the random was more fun.--------//
            //This is to check if the drop origin for the item exists, and on the off-chance it doesn't, it'll just spawn at the object's origin.
            // Vector3 spawnPosition = dropOrigin.position != null ? dropOrigin.position : transform.position;
            // Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
        //----------------------------------------------------------------------------------------------------------------------------------//
        
        //Get rid of the original object.
        Destroy(gameObject);
    }
}
