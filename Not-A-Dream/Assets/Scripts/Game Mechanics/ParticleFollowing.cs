using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowing : MonoBehaviour {

	public GameObject desiredParticlePos;
	[Space (10)]
	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for smoothing
	public float timer_x;
	[Space (10)]
	public bool isMouse; //Tick for Mouse particle
	private Vector3 mousePosition;
	[Space (10)]
	public bool FollowShape;
	public Path_Manager PathToFollow;
	public int CurrentWaypointID;
	public float FollowSpeed;
	public float FollowReachDistance = 1.0f;
	public string PathName;
	private float FollowDistance;
	[Space (10)]
	public bool isRune_5;
	public string Rune_5_State;
	public Path_Manager PathToFollow_2;

	[Space (20)]

	public bool isPlatform;
	public bool PlatformEnabled;
	[Space (10)]
	[Range (0, 10)]
	public int platformSpeed;

	private Vector3 current_position;

	void Update () {
		if (!PauseMenu.GameIsPaused) {
			if ((isMouse) && (!FollowShape) && (!isRune_5) && (!isPlatform)) {
				mousePosition = Input.mousePosition;
				mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

				float position_x = Mathf.SmoothDamp (transform.position.x, mousePosition.x, ref velocity.x, timer_x);
				float position_y = Mathf.SmoothDamp (transform.position.y, mousePosition.y, ref velocity.y, timer_y);
				transform.position = new Vector3 (position_x, position_y, 0);

			} else if ((FollowShape) && (!isMouse) && (!isRune_5) && (!isPlatform)) {
				float position_x = Mathf.SmoothDamp (transform.position.x, PathToFollow.path_objs[CurrentWaypointID].position.x, ref velocity.x, timer_x);
				float position_y = Mathf.SmoothDamp (transform.position.y, PathToFollow.path_objs[CurrentWaypointID].position.y, ref velocity.y, timer_y);
				transform.position = new Vector3 (position_x, position_y, 0);

				FollowDistance = Vector3.Distance (PathToFollow.path_objs[CurrentWaypointID].position, transform.position);

				if (FollowDistance <= FollowReachDistance) {
					CurrentWaypointID++;
				}
				if (CurrentWaypointID >= PathToFollow.path_objs.Count) {
					CurrentWaypointID = 0;
				}
			} else if ((isRune_5) && (!FollowShape) && (!isMouse) && (!isPlatform)) {
				if (Rune_5_State == "Charging_1") {
					float position_x = Mathf.SmoothDamp (transform.position.x, PathToFollow.path_objs[CurrentWaypointID].position.x, ref velocity.x, timer_x);
					float position_y = Mathf.SmoothDamp (transform.position.y, PathToFollow.path_objs[CurrentWaypointID].position.y, ref velocity.y, timer_y);
					transform.position = new Vector3 (position_x, position_y, 0);

					FollowDistance = Vector3.Distance (PathToFollow.path_objs[CurrentWaypointID].position, transform.position);
				}
				if (Rune_5_State == "Charging_2") {
					float position_x = Mathf.SmoothDamp (transform.position.x, PathToFollow_2.path_objs[CurrentWaypointID].position.x, ref velocity.x, timer_x);
					float position_y = Mathf.SmoothDamp (transform.position.y, PathToFollow_2.path_objs[CurrentWaypointID].position.y, ref velocity.y, timer_y);
					transform.position = new Vector3 (position_x, position_y, 0);

					FollowDistance = Vector3.Distance (PathToFollow_2.path_objs[CurrentWaypointID].position, transform.position);
				}
				if (Rune_5_State == "Merging") {
					float position_x = Mathf.SmoothDamp (transform.position.x, desiredParticlePos.transform.position.x, ref velocity.x, timer_x);
					float position_y = Mathf.SmoothDamp (transform.position.y, desiredParticlePos.transform.position.y, ref velocity.y, timer_y);
					transform.position = new Vector3 (position_x, position_y, 0);
				}
			} else if ((isPlatform) && (!isRune_5) && (!FollowShape) && (!isMouse)) {
				if (PlatformEnabled) {
					transform.position = Vector3.MoveTowards (transform.position, PathToFollow.path_objs[CurrentWaypointID].position, platformSpeed * Time.deltaTime);

					FollowDistance = Vector3.Distance (PathToFollow.path_objs[CurrentWaypointID].position, transform.position);

					if (FollowDistance <= 0.01f) {
						CurrentWaypointID++;
					}
					if (CurrentWaypointID >= PathToFollow.path_objs.Count) {
						CurrentWaypointID = 0;
					}
				}
			} else {
				if (desiredParticlePos != null) { //To prevent NullReferenceException Error
					float position_x = Mathf.SmoothDamp (transform.position.x, desiredParticlePos.transform.position.x, ref velocity.x, timer_x);
					float position_y = Mathf.SmoothDamp (transform.position.y, desiredParticlePos.transform.position.y, ref velocity.y, timer_y);
					transform.position = new Vector3 (position_x, position_y, 0);
				}
			}
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if ((isPlatform) && (!isRune_5) && (!FollowShape) && (!isMouse)) {
				other.collider.transform.SetParent (transform);
			}
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if ((isPlatform) && (!isRune_5) && (!FollowShape) && (!isMouse)) {
				other.collider.transform.SetParent (null);
			}
		}
	}
}