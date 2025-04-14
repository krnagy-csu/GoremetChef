using UnityEngine;


/* Senen Bagos
 * this class is the parent of all counters that holds the basic methods
 * controlling the basic interaction of placing or pickup on the counter
 *
 */
public class BaseCounter : MonoBehaviour {
    
    public Transform countTopPoint;
    private GameObject thingOnCounter;

    public Transform getCountTopPoint() {
        return countTopPoint;
    }
    
    public void setThingOnCounter(GameObject ingredient) {
        if (ingredient == null) {
            Debug.Log("Nothing in your inventory");
            return;
        }

        if (hasObjectOnCounter()) {
            Debug.Log("Something is already on your the counter "+ thingOnCounter.name);
            return;
        }
        
        thingOnCounter = ingredient;
    }
    
    public void clearObjectOnCounter() {
        thingOnCounter = null;
    }
    
    public bool hasObjectOnCounter() {
        return thingOnCounter != null;
    }

}
