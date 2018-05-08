using UnityEngine;

public class ConstantRotation : MonoBehaviour {

    public float speed = 2;
    private Vector3 up;

	void Start () {
        up = Vector3.up;
	}
	
	void Update () {
        //transform.Rotate(up * Time.deltaTime * speed);
        transform.RotateAround(up, Time.deltaTime * speed);
	}
}
