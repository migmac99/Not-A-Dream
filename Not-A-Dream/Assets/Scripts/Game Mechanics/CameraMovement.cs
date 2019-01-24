using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for camera smoothing
	public float timer_x;

	public float timer_y_Bubble; //Timers for camera smoothing when using bubble power
	public float timer_x_Bubble;

	//public float GameManager.Instance.DirectorCameraSpeed;
	//public float GameManager.Instance.DirectorCameraSpeed;

	private float position_x;
	private float position_y;

	public GameObject Player;
	public GameObject floor;

	public float floorMargin; //Margin to adjust y axis on camera

	[Space (20)]

	public bool DirectorMode;
	[Space (5)]
	public Path_Manager DirectorPath;
	[Space (5)]
	public int DirectorWaypointID;

	private float DirectorFollowDistance;
	public bool DirectorHasRun = false;

	void Start () {
		transform.position = Player.transform.position;
		DirectorHasRun = false;
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	void FixedUpdate () {
		if (!DirectorMode) {
			if ((Player.GetComponent<RunePowers> ().Rune_1_State == "Active") || (Player.GetComponent<Animator> ().GetAnimatorTransitionInfo (0).IsName ("Bubble_Pop -> playerJump")) || (Player.GetComponent<Animator> ().GetAnimatorTransitionInfo (0).IsName ("Bubble -> Bubble_Pop"))) {
				//Math for smooth camera while in bubble state or during bubble pop animation
				position_x = Mathf.SmoothDamp (transform.position.x, Player.transform.position.x, ref velocity.x, timer_x_Bubble);
				position_y = Mathf.SmoothDamp (transform.position.y, Player.transform.position.y, ref velocity.y, timer_y_Bubble);
				transform.position = new Vector3 (position_x, position_y, -1f);
			} else {
				//Math for smooth camera until it hits desired target
				position_x = Mathf.SmoothDamp (transform.position.x, Player.transform.position.x, ref velocity.x, timer_x);
				// if (floor) {
				// 	position_y = Mathf.SmoothDamp (transform.position.y, (floor.transform.position.y + floorMargin), ref velocity.y, timer_y);
				// }
				position_y = Mathf.SmoothDamp (transform.position.y, (Player.transform.position.y + floorMargin), ref velocity.y, timer_y);
				transform.position = new Vector3 (position_x, position_y, -1f);
			}
		} else {
			//Player.GetComponent<PlayerMovement> ().PlayerPaused = true;
			DirectorFollowDistance = Vector3.Distance (DirectorPath.path_objs[DirectorWaypointID].position, transform.position);
			//DirectorCutTime
			if ((DirectorFollowDistance <= 22f) && (!DirectorHasRun)) {
				DirectorHasRun = true;
				StartCoroutine (Countdown (GameManager.Instance.DirectorCutTime, () => {
					if (DirectorWaypointID >= DirectorPath.path_objs.Count - 1) {
						DirectorWaypointID = 0;
						DirectorMode = false;
						Player.GetComponent<PlayerMovement> ().PlayerPaused = false;
						DirectorPath = null;
					} else {
						DirectorWaypointID++;
					}
					DirectorHasRun = false;
				}));
			}

			float position_x = Mathf.SmoothDamp (transform.position.x, DirectorPath.path_objs[DirectorWaypointID].position.x, ref velocity.x, GameManager.Instance.DirectorCameraSpeed);
			float position_y = Mathf.SmoothDamp (transform.position.y, DirectorPath.path_objs[DirectorWaypointID].position.y, ref velocity.y, GameManager.Instance.DirectorCameraSpeed);
			transform.position = new Vector3 (position_x, position_y, -20);
		}
	}
}