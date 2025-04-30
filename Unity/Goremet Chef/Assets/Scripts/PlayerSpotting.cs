using UnityEngine;

public class PlayerSpotting : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float visibility = 8f;
    public float baseVisibility = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visibility);
    }
    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = gameObject.GetComponent<PlayerMovement>();
        }
        visibility = baseVisibility;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVisibility(float vis)
    {
        visibility = vis;
    }
    public float GetBaseVisiblity()
    {
        return baseVisibility;
    }
    public float GetVisibility()
    {
        return visibility;
    }
}
