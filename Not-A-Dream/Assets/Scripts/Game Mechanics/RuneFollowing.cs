using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneFollowing : MonoBehaviour {

	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for smoothing
	public float timer_x;

	public GameObject desiredRunePos; //Linked to the desired position the Rune will go to

	public float floorMargin; //Margin to adjust y axis
	public float xOffset; //Offset to adjust x axis

	public int ThisRune;

	public GameObject player;
	private PlayerMovement playerMovement;

	void Start () {
		if (GameManager.Instance.UnlockedRune[ThisRune]) {
			transform.position = player.transform.position;
		}
	}

	void FixedUpdate () {
		if (GameManager.Instance.UnlockedRune[ThisRune]) {
			if (player.GetComponent<PlayerMovement> ().Speed > 0) { //Setting the x offset to behind where the player is looking
				xOffset = -Math.Abs (xOffset);
			} else {
				xOffset = Math.Abs (xOffset);
			}
			//Math for the smoothing movement until it hits the desired target
			float position_x = Mathf.SmoothDamp (transform.position.x, desiredRunePos.transform.position.x + xOffset, ref velocity.x, timer_x);
			float position_y = Mathf.SmoothDamp (transform.position.y, (desiredRunePos.transform.position.y + floorMargin), ref velocity.y, timer_y);
			transform.position = new Vector3 (position_x, position_y, 0);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		GameManager.Instance.UnlockedRune[ThisRune] = true;
	}

}