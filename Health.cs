using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public RectTransform healthBar;
	public RPGActor actor;
	
	// Update is called once per frame
	void Update () {
		healthBar.sizeDelta = new Vector2(
			actor.health, 
			healthBar.sizeDelta.y);
	}
}
