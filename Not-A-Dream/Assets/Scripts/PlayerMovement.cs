using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject Player;

	private Rigidbody2D rb;
	private Animator animator;
	private GameObject main_camera;
	private CameraMovement cameraMovement;

	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float checkRadius;
	private float moveInput;

	private bool facingRight = true;

	public float Speed;

	public float JumpForce = 0f;
	public bool hasJumped;

	public bool isGrounded; //Check if player is touching the floor

	void Awake () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		main_camera = GameObject.FindGameObjectWithTag ("MainCamera"); //Referencing to the Main_Camera object with a GameObject tag
	}

	void Update () {
		//Player can move if he is alive and is not being hurt
		if ((main_camera.GetComponent<GameManager> ().playerHealth > 0) && (!main_camera.GetComponent<GameManager> ().animator.GetCurrentAnimatorStateInfo (0).IsName ("Hurt"))) {
			KeyboardInput ();
		}

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround); //Check when player is touching the floor

		if (isGrounded) {
			hasJumped = false;
			animator.SetBool ("playerJump", false);
		} else {
			animator.SetBool ("playerJump", true);
		}
		if ((!facingRight && Speed > 0) || (facingRight && Speed < 0)) {
			Flip ();
		}
	}

	void FixedUpdate () {
		rb.velocity = new Vector2 (moveInput * Speed, rb.velocity.y); //Jump speed mechanic for smooth jump
	}

	void KeyboardInput () {
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			animator.SetBool ("playerRun", true); //Plays the PlayerRun animation
			this.transform.position += new Vector3 (Speed * Time.deltaTime, 0, 0); //Movement using speed value
		}
		if (Input.GetKey (KeyCode.A)) {
			Speed = -Math.Abs (Speed); //Sets Speed to the negative value of its absolute value (result is always negative)
		} else if (Input.GetKey (KeyCode.D)) {
			Speed = Math.Abs (Speed); //Sets Speed to the positive value of its absolute value (result is always positive)
		} else {
			animator.SetBool ("playerRun", false); //Stops playing the PlayerRun animation
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (isGrounded) {
				rb.velocity = Vector2.up * JumpForce; //Simple single jump mechanic
			}
		}
	}

	void Flip () { //Flip the character sprite
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Floor") {
			main_camera.GetComponent<CameraMovement> ().floor = other.gameObject; //Defining the floor variable inside CameraMovement script in the mainCamera object to the object that triggered this collision (the last floor the player standed on)
		}
	}
}