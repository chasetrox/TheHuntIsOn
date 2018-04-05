using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 
 * This script places footsteps down on any surface whose layer is "Ground"
 * Attach this script to the player, provide the transform of a gameobject at the player's feet, and provide
 * the prefab to be instantiated.
 */

public class Footsteps : MonoBehaviour
{

    public GameObject footstepPrefab;
    public Transform footPos;
    public float footDistance = 1;
    public float groundCheckDist = 0.2f;
    private Vector3 lastPos;
    private float displacement;
    private Vector3 down;
    private int mask;

    private void Start()
    {
        lastPos = transform.position;
        down = Vector3.down;
        mask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update ()
    {
        // Displacement from last footprint
        displacement = Vector3.Distance(lastPos, transform.position);

        // If we've been displaced further than threshold, check if we can spawn
        // a new footprint
        if (displacement > footDistance)
        {
            lastPos = transform.position;

            // Raycast down from player feet, check if it hits ground
            RaycastHit hit;
            if (Physics.Raycast(footPos.position, down, out hit, groundCheckDist, mask))
            {
                // Instantiate a footstep prefab in the correct place and orientation
                Vector3 pos = hit.point;
                pos.y += 0.1f;
                Vector3 dir = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                Quaternion rot = Quaternion.LookRotation(dir, hit.normal);
                GameObject step = Instantiate(footstepPrefab, pos, rot);

            }
        }
        
	}

}
