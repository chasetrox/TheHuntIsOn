using UnityEngine;
using UnityEngine.Networking;

/* Alex Pantuck
 * Comp50
 * 4/16/18
 * Footsteps.cs
 * 
 * This script places footsteps down on any surface whose layer is "Ground"
 * Attach this script to the player, provide the transform of a gameobject at the player's feet, and provide
 * the prefab to be instantiated. Also provide SFX for footsteps.
 * Also, to make water footsteps work, add a trigger collider to water and give it the tag "water"
 * Also, in edit->project settings->input make sure there is a button called "walk" I recommend shift as its button.
 * 
 * Sounds and prefab instantiation is determined by a displacement, so these are not networked.
 * What IS networked, however, is the "walk" state--if a player is holding the walk button,
 * they move slower, but provide no footprints or footstep sounds
 */

[RequireComponent(typeof(AudioSource))]
public class Footsteps : NetworkBehaviour
{

    public GameObject footstepPrefab; // Prefab of footprint sprites
    public GameObject waterSplashPrefab;
    public Transform footPos; // Position of player feet, where to check ground from
    public float footDistance = 1; // Minimum displacement to place a footprint and play sound
    public float groundCheckDist = 0.2f; // How far down below feet to check for ground
    public AudioClip[] snowSFX; // Sounds of walking in snow
    public AudioClip[] waterSFX; // Sounds of walking in water

    [SyncVar] private bool isWalking = false;
    [SyncVar] private bool inWater = false;
    private Vector3 lastPos;
    private float displacement;
    private Vector3 down;
    private int groundMask; // Layer mask for ground
    private int waterMask; // Layer mask for water

    private void Start()
    {
        lastPos = transform.position;
        down = Vector3.down;
        groundMask = 1 << LayerMask.NameToLayer("Ground");
        waterMask = 1 << LayerMask.NameToLayer("Water");
    }

    void Update()
    {
        if (isLocalPlayer)
            isWalking = Input.GetButton("walk");

        if (isWalking)
            return;

        // Displacement from last footprint
        displacement = Vector3.Distance(lastPos, transform.position);

        // If we've been displaced further than threshold, check if we can spawn
        // a new footprint
        if (displacement > footDistance)
        {
            // Update last known position
            lastPos = transform.position;
            // Play footstep sound
            playSound();

            // Dont place footsteps while standing in water
            if(!inWater)
            {
                // Raycast down from player feet, check if it hits ground
                RaycastHit hit;
                if (Physics.Raycast(footPos.position, down, out hit, groundCheckDist, groundMask))
                {
                    // Instantiate a footstep prefab in the correct place and orientation
                    Vector3 pos = hit.point;
                    pos.y += 0.1f;
                    Vector3 dir = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                    Quaternion rot = Quaternion.LookRotation(dir, hit.normal);
                    GameObject step = Instantiate(footstepPrefab, pos, rot);
                    Debug.DrawRay(hit.point, Vector3.up, Color.red, 10);
                }
            }
            // But do put water splashes
            else
            {
                // Raycast down from player torso, check if it hits water
                RaycastHit hit;
                if (Physics.Raycast(transform.position, down, out hit, 3, waterMask))
                {
                    // Instantiate a water splash in the correct place
                    Vector3 pos = hit.point;
                    Instantiate(waterSplashPrefab, pos, transform.rotation);
                    Debug.DrawRay(hit.point, Vector3.up, Color.red, 10);
                }
            }
            
        }

    }

    // Play a random sfx from appropriate array of sfx
    private void playSound()
    {
        if (inWater && waterSFX.Length > 0)
        {
            int index = Random.Range(0, waterSFX.Length);
            GetComponent<AudioSource>().PlayOneShot(waterSFX[index]);
        }
        else if (snowSFX.Length > 0)
        {
            int index = Random.Range(0, snowSFX.Length);
            GetComponent<AudioSource>().PlayOneShot(snowSFX[index]);
        }
    }

    public void toggle_water(bool state)
    {
        inWater = state;
    }
}