using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactReceiver : MonoBehaviour {
	public float mass = 3.0f;
	private Vector3 impact = Vector3.zero;
	private CharacterController character;

	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController>();
	}
	
	public void AddImpact(Vector3 impactDir, float force){
	    impactDir.Normalize();
	    if (impactDir.y < 0) impactDir.y = -impactDir.y; // reflect down force on the ground
	   		impact += impactDir.normalized * force / mass;
	 }
 
    void Update(){
	    // apply the impact force:
	    if (impact.magnitude > 0.2) 
	   		character.Move(impact * Time.deltaTime);
	    // consumes the impact energy each cycle:
	    impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }
} 
