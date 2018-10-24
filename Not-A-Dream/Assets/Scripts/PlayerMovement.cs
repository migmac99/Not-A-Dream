using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float Speed;
	public float JumpForce = 0f;

	private float moveInput;
	private Rigidbody2D rb;
	private float jumpTimeCounter;
	public float jumpTime;
	private bool isJumping;
	private Animator animator;

	private bool facingRight = true;
	private bool isGrounded; //Check if player is touching the floor
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	private int extraJumps;
	public int extraJumpsValue;
	public float jumpSpeed;

	public GameObject mainCamera;
	private CameraMovement cameraMovement;

	void Awake () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera"); //Referencing to the CameraMovement script inside the Main_Camera object with a GameObject tag
	}

	void Start () {
		extraJumps = extraJumpsValue;
	}

	//Update is called once per frame
	void Update () {
		KeyboardInput ();

		if (isGrounded) { 
			extraJumps = extraJumpsValue;
			animator.SetBool ("playerJump", false);
		} else {
			animator.SetBool ("playerJump", true);
			return;
		}
	}

	void KeyboardInput () {

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			animator.SetBool ("playerRun", true); //Plays the PlayerRun animation
			this.transform.position += new Vector3 (Speed, 0, 0); //Movement using speed value

			if (Input.GetKey (KeyCode.A)) {
				Speed = -Math.Abs (Speed); //Sets Speed to the negative value of its absolute value (result is always negative)
			} else if (Input.GetKey (KeyCode.D)) {
				Speed = Math.Abs (Speed); //Sets Speed to the positive value of its absolute value (result is always positive)
			}
		} else {
			animator.SetBool ("playerRun", false); //Stops playing the PlayerRun animation
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (extraJumps > 0) { //Double Jump Mechanic
				rb.velocity = Vector2.up * JumpForce;
				extraJumps -= 1;
			}
			if (isGrounded) {
				isJumping = true;
				jumpTimeCounter = jumpTime;
			}
		}
		if (Input.GetKey (KeyCode.Space)) {
			if (isJumping == true) {
				if (jumpTimeCounter > 0) {
					rb.velocity = Vector2.up * JumpForce; //Smooth Jump
					jumpTimeCounter -= Time.deltaTime;
				} else {
					isJumping = false;
					return;
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			isJumping = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		mainCamera.GetComponent<CameraMovement> ().floor = other.gameObject; //Defining the floor variable inside CameraMovement script in the mainCamera object to the object that triggered this collision (the last floor the player standed on)
	}

	void Flip () { //Flip the character sprite
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

	void FixedUpdate () {
		rb.velocity = new Vector2 (moveInput * Speed, rb.velocity.y);

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround); //Check when player is touching the floor

		if (facingRight == false && Speed > 0) {
			Flip ();
		} else if (facingRight == true && Speed < 0) {
			Flip ();
		}
	}

}