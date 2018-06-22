using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

	public float xSpeed = 1;
	public int stop = 5;
	public GameObject player;
	void Start(){
		Invoke("StopXSpeed",stop);
	}

	void Update () {
		float translation = Time.deltaTime * xSpeed;
		transform.Translate (translation, 0, 0);
		if (!(player.gameObject.GetComponent<Renderer> ().isVisible)) {
			//Debug.Log ("Is invisible");
			player.transform.position = new Vector2 (transform.position.x, transform.position.y);
		} else {
			//Debug.Log ("Is visible");
		}
	}

	void StopXSpeed(){
		xSpeed = 0;
	}
}
