using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controls : MonoBehaviour
{

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool duck = false;
	[HideInInspector] public bool slide = false;
	[HideInInspector] public bool wallgrab = false;
	public float moveForce = 365f;
	public float maxSpeed = 3f,maxSpeedVertical=15;
	public float jumpForce = 1000f,wallJumpMultiplierY = 2f,wallJumpMultiplierX = 1.5f;
	public float serverInit=1f, serverTimeout=0.5f,slideDuration=0.5f,wallJumpControlFreze=0.1f;
	public Transform groundCheck;
	private bool controlsFrozen = false;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		InvokeRepeating("updateServer",serverInit,serverTimeout);
	}
	void updateServer(){
		float h = Input.GetAxis ("Horizontal");
		udpClient.sendMessage (transform.position.x+","+transform.position.y+";"+(facingRight?1:0));
	}
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		if(grounded)
			anim.SetTrigger ("Land");
		if (Input.GetButtonDown ("Jump") && (grounded||wallgrab)) {
			jump = true;
		}
	}

	void FixedUpdate ()
	{

		if(controlsFrozen)
			return;
		//Get axis information and inform the animator
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (h));

		//Stop movement if ducking
		if (duck)
			h = 0;

		//Apply velocity
		if (h * rb2d.velocity.x < maxSpeed )
			rb2d.AddForce (Vector2.right * h * moveForce);

		//Limit velocity
		if (h == 0&&!slide)
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		if (Mathf.Abs (rb2d.velocity.y) > maxSpeedVertical)
			rb2d.velocity = new Vector2 (rb2d.velocity.x,Mathf.Sign (rb2d.velocity.y) * maxSpeedVertical);
		
		//Flip sprites
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();
		//Apply Jump Foce
		if (jump) {
			anim.SetTrigger ("Jump");
			if (wallgrab) {
				rb2d.AddForce (new Vector2 ((facingRight?-1:1) * moveForce * wallJumpMultiplierX, jumpForce * wallJumpMultiplierY));
				controlsFrozen = true;
				Invoke ("DefrezeControls", wallJumpControlFreze);
			} else 
				rb2d.AddForce (new Vector2 (0, jumpForce));
			
			jump = false;
		}
		//Ducking mechanic
		duck = false;
		slide = false;
		anim.SetBool ("Duck", false);
		if (Input.GetKey (KeyCode.S)) {
			GetComponent<BoxCollider2D> ().enabled = false;
			if (Mathf.Abs (h) > 0.1) {
				slide = true;
				Debug.Log ("Slide");
				anim.SetTrigger ("Slide");
				Invoke ("EnableBoxCollider", slideDuration);
			} else {
				duck = true;
				Debug.Log ("Duck");
				anim.SetBool ("Duck", true);
			}
		} else if(!slide)
			GetComponent<BoxCollider2D> ().enabled = true;
		
	}
	void EnableBoxCollider(){
		GetComponent<BoxCollider2D> ().enabled = true;
	}
	void DefrezeControls(){
		controlsFrozen = false;
	}
	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("TriggerEntered");
		if (!grounded) {
			wallgrab = true;
			anim.SetBool ("WallGrab", true);
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		Debug.Log ("TriggerExit");
		wallgrab = false;
		anim.SetBool ("WallGrab", false);
	}
}
	