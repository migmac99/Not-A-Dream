using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowing : MonoBehaviour {

	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for smoothing
	public float timer_x;

	public GameObject desiredRunePos;

	void Update () {
		float position_x = Mathf.SmoothDamp (transform.position.x, desiredRunePos.transform.position.x, ref velocity.x, timer_x);
		float position_y = Mathf.SmoothDamp (transform.position.y, (desiredRunePos.transform.position.y), ref velocity.y, timer_y);
		transform.position = new Vector3 (position_x, position_y, 0);
	}
}