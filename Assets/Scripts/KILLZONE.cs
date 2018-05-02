using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 4/28/18
 * KILLZONE.cs
 * 
 * Immediately kills anyone who goes into this trigger
 * 
 * Note: When I wrote this, I hadnt imported PlayerHealth,
 * so you'll need to uncomment that line to make it work
 * 
 */

public class KILLZONE : MonoBehaviour {

    // Assuming the only thing that will entire this zone is a player
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>().TakeDamage(999);
    }

}
