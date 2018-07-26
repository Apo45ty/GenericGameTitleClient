using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

	public RPGActor player;
	public float offscreenDamage = 10;

	void Update () {
		if (!(player.gameObject.GetComponent<Renderer> ().isVisible)) {
			//Debug.Log ("Is invisible");
			player.health -= offscreenDamage;
			player.transform.position = new Vector2 (transform.position.x, transform.position.y);
		} else {
			//Debug.Log ("Is visible");
		}
	}
}
