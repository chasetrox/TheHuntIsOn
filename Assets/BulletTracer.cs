using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracer : MonoBehaviour {
	//public Transform spawnPosition;
    public float speed = 1.0F;
    public GameObject bullet;

    private float startTime;
    private float journeyLength;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private bool isMoving;

    void Start() {
        //bullet.transform.position = spawnPosition.position;
    }

    void Update() {
    	if (isMoving)
    	{
	        float distCovered = (Time.time - startTime) * speed;
	        float fracJourney = distCovered / journeyLength;
	        bullet.transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
	        if (bullet.transform.position == endMarker) 
	        {
	        	Destroy(bullet);
	        }
	    }
    }

    public void shoot(Vector3 begin, Vector3 end)
    {
    	startTime = Time.time;
        journeyLength = Vector3.Distance(begin, end);
        bullet.transform.position = begin;
        isMoving = true;
        startMarker = begin;
        endMarker = end;
    }
}
