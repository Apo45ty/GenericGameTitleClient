using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class remoteplayercontroller: UDPListener{
	public bool facingRight = true;
	public bool jump = false;
	private bool grounded = false;
	private Animator anim;
	private bool updatePosition = false;
	private float x=0,y=0 ;
	// Use this for initialization
	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	public override void UDPMessageReceived(string message){
		Debug.Log ("Listener:" + message);
		string[] data = message.Split (new char[] {char.Parse(";")},StringSplitOptions.None);
		Debug.Log (data[0]);
		string coordinatesString = data [0];
		string[] coordinatesData = coordinatesString.Split (new char[] {char.Parse(",")},StringSplitOptions.None);
		x = float.Parse(coordinatesData[0]);
		y = float.Parse(coordinatesData[1]);
		updatePosition = true;
		string facingString = data [1];
		int facingInt = int.Parse (facingString);
		facingRight = facingInt>0?true:false;
	}

	void FixedUpdate ()
	{
		if (updatePosition) {
			anim.SetFloat ("Speed", Mathf.Abs (transform.position.x-x));
			transform.position = Vector3.Slerp (
				new Vector3(transform.position.x,transform.position.y,transform.position.z), 
				new Vector3(x,y,transform.position.z), 0.5f);
			if (facingRight)
				FaceRight ();
			else
				FaceLeft ();
			updatePosition = false;
		}
		if (jump) {
			anim.SetTrigger ("Jump");
		}
	}

	void FaceLeft()
	{
		Vector3 theScale = transform.localScale;
		theScale.x = Mathf.Sign(theScale.x)<0?theScale.x: -1*theScale.x;
		transform.localScale = theScale;
	}
	void FaceRight()
	{
		Vector3 theScale = transform.localScale;
		theScale.x = Mathf.Abs(theScale.x) ;
		transform.localScale = theScale;
	}
}

