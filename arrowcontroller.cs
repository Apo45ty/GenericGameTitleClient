using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowcontroller : MonoBehaviour {
	public float oldx=0, oldy=0, x, y;

	// Use this for initialization
	void Start () {
		x = transform.position.x;
		y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		oldx = x;
		oldy = y;
		x = transform.position.x;
		y = transform.position.y;

		transform.localScale = new Vector3(1, 1, 1);
		if (x == oldx && y == oldy) {
			transform.localScale = new Vector3(0, 0, 0);
		} else if (x == oldx && y > oldy) {
			transform.localEulerAngles = new Vector3(0, 0, 90);
		} else if (x == oldx && y < oldy) {
			transform.localEulerAngles = new Vector3(0, 0, 270);
		} else if (x > oldx && y == oldy) {
			transform.localEulerAngles = new Vector3(0, 0, 0);
		} else if (x < oldx && y == oldy) {
			transform.localEulerAngles = new Vector3(0, 0, 180);
		} else if (x > oldx && y > oldy) {
			transform.localEulerAngles = new Vector3(0, 0, 45);
		} else if (x > oldx && y < oldy) {
			transform.localEulerAngles = new Vector3(0, 0, -45);
		} else if (x < oldx && y > oldy) {
			transform.localEulerAngles = new Vector3(0, 0, 135);
		} else if (x < oldx && y < oldy) {
			transform.localEulerAngles = new Vector3(0, 0, -135);
		} 
	}
}
