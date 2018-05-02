using System.Collections;
using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 4/27/18
 * collectable.cs
 * 
 * This function is attached to the bullet prefabs,
 * and allows them to be picked up if the player
 * has their mouse over it and is within a threshold
 * distance.
 * 
 * When in range and with mouse over the object,
 * Text is displayed with custom settings and position.
 * 
 * Two notes:
 * 1. OnMouseOver works on non-trigger colliders, so
 *    the bullet prefabs have a collider. In order
 *    to make them non-solid, I gave them a separate
 *    layer called "Collectable" and then set it up
 *    in the Physics settings so that only UI can
 *    interact with that layer.
 * 2. As a consequence, it's also really easy to make
 *    only the hunter see the bullets, because you
 *    can just exclude the Collectable layer from
 *    the Beast's camera culling options.
 * 
 */

[RequireComponent(typeof(AudioSource))]
public class collectable : MonoBehaviour {

    public float maxDistance = 10;
    public string toolTip = "(E) Collect";
    public AudioClip collectSound;
    public int textWidth = 300, textHeight = 200;
    public GUIStyle textStyle;

    private int screenWidth, screenHeight;
    private bool inRange = false;
    private AudioSource speaker;
    private bool collected = false;
    
    private void Start()
    {
        screenWidth = Screen.width / 2;
        screenHeight = Screen.height / 2;
        speaker = GetComponent<AudioSource>();
    }

    // If looking at object
    private void OnMouseOver()
    {
        if (collected)
            return;

        // If within range
        if (Vector3.Distance(Camera.main.transform.position, transform.position) <= maxDistance)
        {
            inRange = true;

            // If the collect button is pressed
            if (Input.GetButtonDown("interact"))
            {
                // Dont allow multiple pick-ups
                collected = true;

                // Play collection sound effect
                if (collectSound != null)
                    speaker.PlayOneShot(collectSound);

                // Add bullets to player
                PlayerShooting shooter;
                shooter = Camera.main.GetComponentInParent<PlayerShooting>();
                if (shooter != null)
                    shooter.addBullets(1);

                // Destroy gameobject
                StartCoroutine(DestroyMe());
            }    
        }
        else if (inRange)
            inRange = false;

    }

    // If no longer looking at object, stop drawing text
    private void OnMouseExit()
    {
        inRange = false;
    }

    // Draw text to screen if looking at object and within range
    private void OnGUI()
    {
        if (inRange)
        {
            // Position on screen to draw text
            Rect position = new Rect(screenWidth - (textWidth/2), screenHeight, textWidth, textHeight);
            GUI.Label(position, toolTip, textStyle);
        }     
    }

    // Play pick up sound, stop the particle effect, and then
    // destroy gameobject when the sound is done playing
    private IEnumerator DestroyMe()
    {
        GetComponent<ParticleSystem>().Stop();
        while (speaker.isPlaying)
            yield return null;

        Destroy(gameObject);
        yield return null;
    }
}
