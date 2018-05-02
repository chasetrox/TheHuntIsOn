using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 4/27/18
 * randomizedPlacer.cs
 * 
 * When the scene loads, this script takes the transforms
 * from all of its children, and spawns an object at
 * a randomized set of them. It spawns a random
 * number (from a range) of total objects.
 * 
 * I'm using it currently to locally spawn a random number 
 * of bullets at random positions.
 * 
 */

public class randomizedPlacer : MonoBehaviour {

    // What to spawn
    public GameObject objectPrefab;
    // Minimum and maximum number of objects to spawn
    public int minSpawn = 3, maxSpawn = 10;

    // Where to spawn them
    private Transform[] spawnLocations;

	void Start () {
        // Get all transforms from children
        spawnLocations = GetComponentsInChildren<Transform>();
        int numSpawn = spawnLocations.Length;

        // If max spawn is higher than the number of possible spawn locations, 
        // set it down to the number of spawn locations
        maxSpawn = (maxSpawn > numSpawn) ? numSpawn : maxSpawn;

        // Shuffle the order of spawn locations array
        randomShuffle<Transform>(ref spawnLocations);

        // Randomize number of objects to spawn within parameters
        numSpawn = Random.Range(minSpawn, maxSpawn);

        // Spawn in the objects
        for (int i = 0; i < numSpawn; i++)
        {
            Transform spawn = spawnLocations[i];
            Instantiate(objectPrefab, spawn.position, spawn.rotation, spawn);
        }

        // Unload unused transforms
        for (int i = numSpawn; i < spawnLocations.Length; i++)
            Destroy(spawnLocations[i].gameObject);

        // No reason to keep script loaded
        Destroy(this);
	}
	
    // Generic array shuffler
	private void randomShuffle<T>(ref T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int index = Random.Range(0, n);
            T temp = array[i];
            array[i] = array[index];
            array[index] = temp;
        }
    }
}
