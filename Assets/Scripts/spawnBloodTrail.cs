using UnityEngine;


/* Alex Pantuck
 * spawnBloodTrail.cs
 * 4/16/18
 * 
 * Put this script on the beast to spawn a blood trail
 * every once in a while.
 * 
 */

public class spawnBloodTrail : MonoBehaviour
{
    // The blood trail prefab
    public GameObject bloodTrailPrefab;
    // Where does the trail initially spawn from?
    // (make sure the forward vector lines up with the players forward vector)
    public Transform spawnPosition;
    // Times when bloodTrail spawn rate doubles (in minutes)
    public float[] thresholds = {6, 10};
    // Default spawn rate in minutes
    public float baseSpawnRate = 2;

    // How often to spawn blood trails
    private float spawnRate;
    // Time since the last blood trail spawned
    private float timeSince = 0;

	void Start () {
        if (bloodTrailPrefab == null)
        {
            print("spawnBloodTrail: missing reference to blood prefab. Destroying script.");
            Destroy(this);
        }
        
        if (spawnPosition == null)
        {
            print("spawnBloodTrail: missing reference to spawn position. Destroying script.");
            Destroy(this);
        }

        // Convert to seconds
        baseSpawnRate *= 60;
        for (int i = 0; i < thresholds.Length; i++)
            thresholds[i] *= 60;
	}
	
	void Update ()
    {
        // Determine spawn rate:

        // 10 minutes have elapsed (4 blood trail spawns)
        if (Time.time > thresholds[1])
        {
            // Fastest spawn rate (spawn every 30s)
            spawnRate = baseSpawnRate * 4;
        }
        // 6 minutes have elapsed (3 blood trail spawns)
        else if (Time.time > thresholds[0])
        {
            // Medium spawn rate (spawn every 1 min)
            spawnRate = baseSpawnRate * 2;
        }
        // Fewer than 6 minutes have elapsed, default
        else
        {
            // default speed (spawn every 2 min)
            spawnRate = baseSpawnRate;
        }

        // Every "x" seconds, spawn a new blood trail
        if (Time.time - timeSince >= spawnRate)
        {
            // update the last time since spawn a blood trail
            timeSince = Time.time;
            // spawn the blood trail (locally)
            Instantiate(bloodTrailPrefab, spawnPosition.position, spawnPosition.rotation);
        }

	}
}
