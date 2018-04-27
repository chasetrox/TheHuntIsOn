using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 4/26/18
 * DetectWater.cs
 * 
 * Attach this script to the water gameobject and make sure
 * it has a trigger collider.
 * 
 * When a player walks into water, this script flags them is being
 * in water. This makes it easier to change the sound of footsteps
 * to water splashes, and also prevents footprints in the water.
 * 
 * Make sure that:
 * The hunter is tagged as "Hunter"
 * The beast is tagged as "Beast"
 * Both have the Footsteps script
 * 
 */

public class DetectWater : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hunter") || other.CompareTag("Beast"))
        {
            Footsteps feet = other.GetComponent<Footsteps>();
            if (feet != null)
                feet.toggle_water(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hunter") || other.CompareTag("Beast"))
        {
            Footsteps feet = other.GetComponent<Footsteps>();
            if (feet != null)
                feet.toggle_water(false);
        }
    }
}
