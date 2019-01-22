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
	//public float moveInput;

	public bool facingRight = true;

	public float Speed;

	public float JumpForce = 0f;
	public bool hasJumped;

	public bool isGrounded; //Check if player is touching the floor

	public bool PlayerPaused = false;

	public bool Tutorial;

	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		if (Tutorial) {
			transform.position = GameManager.Instance.TutorialCheckpointPos;
		} else {
			transform.position = GameManager.Instance.CurrentCheckpointPos;
		}
		main_camera = GameObject.FindGameObjectWithTag ("MainCamera"); //Referencing to the Main_Camera object with a GameObject tag
	}

	void Update () {
		//Player can move if he is alive and is not being hurt
		if ((GameManager.Instance.PlayerHealth > 0) && (!main_camera.GetComponent<PlayerManager> ().animator.GetCurrentAnimatorStateInfo (0).IsName ("Hurt"))) {
			KeyboardInput ();
		}

		if (!PlayerPaused) {
			rb.gravityScale = 3;

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
		} else {
			rb.gravityScale = 0;
			rb.velocity = new Vector2 (0, 0);
		}
	}

	void FixedUpdate () {
		if (!PlayerPaused) {
			rb.velocity = new Vector2 (0, rb.velocity.y); //Jump speed mechanic for smooth jump
		}
	}

	void KeyboardInput () {
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			if (!PlayerPaused) {
				animator.SetBool ("playerRun", true); //Plays the PlayerRun animation
				this.transform.position += new Vector3 (Speed * Time.deltaTime, 0, 0); //Movement using speed value
			}
		}
		if (Input.GetKey (KeyCode.A)) {
			if (!PlayerPaused) {
				Speed = -Math.Abs (Speed); //Sets Speed to the negative value of its absolute value (result is always negative)
			}
		} else if (Input.GetKey (KeyCode.D)) {
			if (!PlayerPaused) {
				Speed = Math.Abs (Speed); //Sets Speed to the positive value of its absolute value (result is always positive)
			}
		} else {
			if (!PlayerPaused) {
				animator.SetBool ("playerRun", false); //Stops playing the PlayerRun animation
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
	}

	public void Jump () {
		if (isGrounded) {
			rb.velocity = Vector2.up * JumpForce; //Simple single jump mechanic
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