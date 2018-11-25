using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePowers : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator animator;

	public GameObject Player;

	public GameObject Rune_1;
	public GameObject Rune_2;
	public GameObject Rune_3;
	public GameObject Rune_4;
	public GameObject Rune_5;

	public float Rune_1_Timeout; //In Seconds
	public float Rune_1_CurrentTime = 0;
	public string Rune_1_State = "Idle";
	public float Rune_1_UpForce;

	void Awake () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (Rune_1.GetComponent<RuneFollowing> ().isCollected) { //If the rune has been collected (Reference to the RuneFollowing script inside this rune)
			Power_1 ();
		}
	}

	void Power_1 () {
		if (Rune_1_CurrentTime > 0) { //Countdown from Timeout to 0 using real time (regardless of framerate)
			Rune_1_CurrentTime -= (1 * Time.deltaTime);
		} else if (Rune_1_CurrentTime <= 0) { //When CurrentTime reaches 0 then the bubble can be activated again 
			Rune_1_CurrentTime = 0;
			Rune_1_State = "Bubble_Ready";
			animator.SetBool ("bubble", false);
		}
		if (Input.GetKeyUp (KeyCode.Space)) { //When space is released and player on the air then say he has jumped before
			if (!Player.GetComponent<PlayerMovement> ().isGrounded) {
				Player.GetComponent<PlayerMovement> ().hasJumped = true;
			}
			Rune_1_State = "Idle"; //When space is released the Bubble will be disabled
			animator.SetBool ("bubble", false);
		}
		if (Input.GetKey (KeyCode.Space)) { //When Space is pressed and the cooldown is reset and player on the air activate bubble and set timeout
			if ((Rune_1_State == "Bubble_Ready") && (!Player.GetComponent<PlayerMovement> ().isGrounded) && (Player.GetComponent<PlayerMovement> ().hasJumped)) {
				Rune_1_CurrentTime = Rune_1_Timeout;
				Rune_1_State = "Bubble_Active";
			}
		}
		if (Rune_1_State == "Bubble_Active") { //Bubble going up
			rb.velocity = Vector2.up * Rune_1_UpForce;
			Player.GetComponent<PlayerMovement> ().hasJumped = false;
			animator.SetBool ("bubble", true);
		}
	}
}