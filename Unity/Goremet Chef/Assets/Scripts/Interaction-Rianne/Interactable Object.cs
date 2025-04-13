using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //This is the script that should be on any item a player can hit. Garbage cans, dumpsters, civilians.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This is when the object is hit.
    public void TakeDamage()
    {
        Debug.Log("Ouch! I'm " + gameObject.name);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " destroyed.");
        Destroy(gameObject);
    }
}
