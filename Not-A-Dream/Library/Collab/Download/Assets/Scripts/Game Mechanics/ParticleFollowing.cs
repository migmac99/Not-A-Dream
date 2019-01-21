using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowing : MonoBehaviour {

	public GameObject desiredParticlePos;

	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for smoothing
	public float timer_x;

	public bool isMouse; //Tick for Mouse particle
	private Vector3 mousePosition;

	public bool FollowShape;
	public Path_Manager PathToFollow;
	public int CurrentWaypointID;
	public float FollowSpeed;
	public float FollowReachDistance = 1.0f;
	public string PathName;
	private float FollowDistance;

	public bool isRune_5;

	private Vector3 current_position;

	void Update () {
		if (!PauseMenu.GameIsPaused) {
			if ((isMouse) && (!FollowShape) && (!isRune_5)) {
				mousePosition = Input.mousePosition;
				mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

				float position_x = Mathf.SmoothDamp (transform.position.x, mousePosition.x, ref velocity.x, timer_x);
				float position_y = Mathf.SmoothDamp (transform.position.y, mousePosition.y, ref velocity.y, timer_y);
				transform.position = new Vector3 (position_x, position_y, 0);

			} else if ((FollowShape) && (!isMouse) && (!isRune_5)) {
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
			} else if ((isRune_5) && (!FollowShape) && (!isMouse)) {
				float position_x = Mathf.SmoothDamp (transform.position.x, PathToFollow.path_objs[CurrentWaypointID].position.x, ref velocity.x, timer_x);
				float position_y = Mathf.SmoothDamp (transform.position.y, PathToFollow.path_objs[CurrentWaypointID].position.y, ref velocity.y, timer_y);
				transform.position = new Vector3 (position_x, position_y, 0);

				FollowDistance = Vector3.Distance (PathToFollow.path_objs[CurrentWaypointID].position, transform.position);
				Debug.Log ("TOU VIVOOOOO");

				if (FollowDistance <= FollowReachDistance) {
					CurrentWaypointID++;
				}

				if (CurrentWaypointID >= PathToFollow.path_objs.Count) {
					CurrentWaypointID = 0;
					Debug.Log ("ZERO");
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
}