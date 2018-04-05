using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Alex Pantuck
 * Comp50
 * 
 * This script is intended to be used on the footstep prefab, which is composed
 * of a parent holding two children with sprite renderer objects.
 * 
 * Over the course of its 'lifespan' the sprites alpha channel is faded down linearly as a function
 * of time. At the end of its lifespan, it is destroyed
 */

public class destroyOnTimer : MonoBehaviour
{

    public float lifespan = 30;
    private SpriteRenderer[] rend;
    private float startTime;

	// Use this for initialization
	void Start () {
        rend = GetComponentsInChildren<SpriteRenderer>();
        startTime = Time.time;
        StartCoroutine(killMe());
	}

    private void Update()
    {
        float percent = (Time.time - startTime) / lifespan;

        byte alpha = (byte)(255 - (255 * percent));
        
        foreach(SpriteRenderer r in rend)
        {
            r.color = new Color32(255, 255, 255, alpha);
        }
    }

    IEnumerator killMe()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
