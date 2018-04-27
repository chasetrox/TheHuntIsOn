using UnityEngine;
using UnityEngine.Networking;

/* Alex Pantuck
 * Comp50
 * 4/26/18
 * followParent.cs
 * 
 * This script takes a reference to its parent, 
 * deparents it, and then makes this gameobject
 * follow its parent's x and z coordinates.
 * 
 * This is currently on the snow storm particle effect because:
 * Finding the player through networked means is a pain
 * Keep the particle effect parented causes extra lag because of its rotation.
 * 
 * So this makes it real easy to find and follow the local player
 * 
 */

public class followParent : NetworkBehaviour {

    Transform parent;
    float yPos;
    Vector3 up;

	// Use this for initialization
	void Start () {
        yPos = transform.position.y;
        up = Vector3.up;
        
        // Take reference to parent transform
        parent = transform.parent;

        // Couldnt find parent?
        if (parent == null)
        {
            print("Looks like " + gameObject.name + " doesnt have a parent?");
            Destroy(this);
        }
        // If highest root parent isn't the local player
        else if (!transform.root.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            print("Not local player");
            Destroy(gameObject);
        }
            
        // Detach from parent
        transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = parent.position;
        transform.position += (yPos * up);
	}
}
