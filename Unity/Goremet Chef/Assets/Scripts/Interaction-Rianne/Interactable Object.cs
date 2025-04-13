using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //This is the script that should be on any item a player can hit. Garbage cans, dumpsters, civilians.
    //Interactable objects can take damage from player and drop loot.

    //The loot dropped by the object and where it spawns.
    public GameObject dropPrefab;
    public Transform dropOrigin;

    //This is when the object is hit.
    public void TakeDamage()
    {
        //Hit!
        Debug.Log("Ouch! I'm " + gameObject.name);
        
        //Killed!
        Invoke(nameof(Die), 2f);
    }

    //
    private void Die()
    {
        Debug.Log(gameObject.name + " destroyed.");
        
        //Drop the item.
        //This is to check if the drop origin for the item exists, and on the off-chance it doesn't, it'll just spawn at the object's origin.
        Vector3 spawnPosition = dropOrigin.position != null ? dropOrigin.position : transform.position;
        Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
        
        //Get rid of the original object.
        Destroy(gameObject);
    }
}
