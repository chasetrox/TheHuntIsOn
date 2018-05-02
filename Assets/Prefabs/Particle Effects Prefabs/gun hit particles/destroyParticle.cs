using UnityEngine;

/* Alex Pantuck
 * Comp50
 * destroyParticles.cs
 * 4/17/18
 * 
 * This script is on all particle systems that do not repeat.
 * When the particle system is done playing, this deletes the gameobject.
 * 
 */

public class destroyParticle : MonoBehaviour {

    ParticleSystem system;

	void Start () {
        system = GetComponent<ParticleSystem>();
        if (system == null)
        {
            print("particle destroyer not placed on a particle system");
            Destroy(this);
        }

    }
	
	void Update () {
        if (!system.isPlaying)
            Destroy(gameObject);
	}
}
