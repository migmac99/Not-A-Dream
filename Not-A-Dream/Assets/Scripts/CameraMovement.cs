using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector2 velocity; //Required for smoothing calculations

	public float timer_y; //Timer for camera smoothing
	public float timer_x;

	public GameObject player;
	public GameObject floor;

	public float floorMargin; //Margin to adjust y axis on camera

	private PlayerMovement playerMovement;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player"); //Referencing to the PlayerMovement script inside the Player object
	}

	void FixedUpdate () {
		//Math for the smoothing camera movement until it hits the desired target
		float position_x = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, timer_x);
		float position_y = Mathf.SmoothDamp (transform.position.y, (floor.transform.position.y + floorMargin), ref velocity.y, timer_y);
		transform.position = new Vector3 (position_x, position_y, -1f);
	}

}