using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

	public GameObject player;

	void Update () {
		if (!(player.gameObject.GetComponent<Renderer> ().isVisible)) {
			//Debug.Log ("Is invisible");
			player.transform.position = new Vector2 (transform.position.x, transform.position.y);
		} else {
			//Debug.Log ("Is visible");
		}
	}
}
