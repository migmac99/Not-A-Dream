using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for camera smoothing
	public float timer_x;

	public float timer_y_Bubble; //Timers for camera smoothing when using bubble power
	public float timer_x_Bubble;

	private float position_x;
	private float position_y;

	public GameObject Player;
	public GameObject floor;

	public float floorMargin; //Margin to adjust y axis on camera

	void FixedUpdate () {
		if ((Player.GetComponent<RunePowers> ().Rune_1_State == "Active") || (Player.GetComponent<Animator> ().GetAnimatorTransitionInfo (0).IsName ("Bubble_Pop -> playerJump")) || (Player.GetComponent<Animator> ().GetAnimatorTransitionInfo (0).IsName ("Bubble -> Bubble_Pop"))) {
			//Math for smooth camera while in bubble state or during bubble pop animation
			position_x = Mathf.SmoothDamp (transform.position.x, Player.transform.position.x, ref velocity.x, timer_x_Bubble);
			position_y = Mathf.SmoothDamp (transform.position.y, Player.transform.position.y, ref velocity.y, timer_y_Bubble);
			transform.position = new Vector3 (position_x, position_y, -1f);
		} else {
			//Math for smooth camera until it hits desired target
			position_x = Mathf.SmoothDamp (transform.position.x, Player.transform.position.x, ref velocity.x, timer_x);
			if (floor) {
				position_y = Mathf.SmoothDamp (transform.position.y, (floor.transform.position.y + floorMargin), ref velocity.y, timer_y);
				transform.position = new Vector3 (position_x, position_y, -1f);
			}

			//print("player " + Player);
			//print("floor " + floor);
		}
	}

}