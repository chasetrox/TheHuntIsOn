using UnityEngine;
using UnityEngine.Networking;

/* Alex Pantuck
 * Comp50
 * 4/27/18
 * spawnBeastRoar.cs
 * 
 * Every two minutes, spawn a local copy
 * of the beast roar prefab.
 * 
 * This script is attached to the Hunter,
 * and gives them a rough direction of the
 * Beast every ~2.5 minutes.
 * 
 */

public class spawnBeastRoar : NetworkBehaviour {

    public GameObject roarPrefab;
    // Every x minutes, spawn a roar
    public float timePerRoar = 2.5f;

    private float timeSince = 0;

	void Start () {
        timePerRoar *= 60;
	}
	
	void Update () {
        // Dont spawn the roar and beast's client
        if (!isLocalPlayer)
            return;

        // If "timPerRoar" seconds have elapsed since the last beast roar placement
		if ((Time.time - timeSince) > timePerRoar)
        {
            // Update the last time a roar was spawned
            timeSince = Time.time;

            // Spawn roar at Beast's position
            GameObject beast = GameObject.FindGameObjectWithTag("Beast");
            if (beast == null)
                print("Error finding beast in the roar spawner");
            else
                Instantiate(roarPrefab, beast.transform.position, beast.transform.rotation);
        }
	}
}
