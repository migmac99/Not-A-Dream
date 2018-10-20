using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float Speed = 0f;
	public float JumpForce = 0f;

	private float moveInput;
	private Rigidbody2D rb;
	
	private bool facingRight = true;
	private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	private int extraJumps;
	public int extraJumpsValue;
	public float jumpSpeed;

	public GameObject floorTag;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Start(){
		extraJumps = extraJumpsValue;
	}


	// Update is called once per frame
	void Update () {
		KeyboardInput ();

		if (isGrounded) {
			extraJumps = extraJumpsValue;
		}
	}

	void KeyboardInput () {

		if (Input.GetKey (KeyCode.D)) {
			this.transform.position += new Vector3 (Speed, 0, 0);
			Speed = 0.01f;

		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.position += new Vector3 (Speed, 0, 0);
			Speed = -0.01f;

		}
		if (Input.GetKey (KeyCode.S)) {

		}

		if (Input.GetKeyDown (KeyCode.Space) && extraJumps > 0) {
			rb.velocity = Vector2.up * JumpForce;
			extraJumps -= 1;
		} else if (Input.GetKeyDown (KeyCode.Space) && extraJumps == 0 && isGrounded) {
			rb.velocity = Vector2.up * JumpForce;
		}

	}

	void OnTriggerEnter2D (Collider2D other) {
		floorTag = other.gameObject;
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

	void FixedUpdate () {

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround);

		if (facingRight == false && Speed > 0) {
			Flip ();
		} else if (facingRight == true && Speed < 0) {
			Flip ();
		}
	}

}