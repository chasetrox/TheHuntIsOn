using UnityEngine;

/* Alex Pantuck
 * 4/10/18
 * bloodMovement.cs
 * 
 * Rotates the GameObject towards its destination,
 * and moves it forward.
 * It accelerates in both its rotation and movement speed.
 * After the lifespan of the object has been reached (or if it
 * reaches its destination), the gameobject is destroyed.
 * Only the beast sees the blood trail, so this is not a networked script.
 * 
 * The hunter must be tagged as "Hunter" for the blood to find it
 * 
 */

public class bloodMovement : MonoBehaviour
{

    public Transform target;
    private Vector3 _target;
    public float moveSpeed, lifespan = 5, swingSpeed = 20, rotationSpeed = 2;

    private float startTime;
    private float swing = 0;
    private float step;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Hunter").transform;

        if (target == null)
        {
            print("Error finding the hunter, bloodMovement.cs");
            Destroy(gameObject);
        }

        _target = target.position;
        startTime = Time.time;
        step = rotationSpeed * Time.deltaTime;
	}
	
	void Update () {
        // Gradually rotate towards destination:
        step *= 1.001f; // Slowly accelerate
        Vector3 destDir = _target - transform.position; // Direction to the destination
        Vector3 newDir = Vector3.RotateTowards(transform.forward, destDir, step, 0.0f); // Intermediate direction
        transform.rotation = Quaternion.LookRotation(newDir); // Look at Intermediate direction

        // Move forward
        moveSpeed *= 1.009f; // Slowly accelerate
        transform.position += transform.forward * moveSpeed * Time.deltaTime; // Move in the forward direction

        // Swing back and forth
        swing += Time.deltaTime * swingSpeed;
        float backAndForth = Mathf.Sin(swing) * 0.1f;
        transform.Translate(backAndForth, 0, 0);

        // Destroy if close to destination or if lifespan ended
        if (Time.time - startTime >= lifespan || Vector3.Distance(transform.position, _target) < 1)
            Destroy(gameObject);
        
	}
}
