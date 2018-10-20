using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Vector2 velocity; //required for calculations

	public float timer_y; //Timer for camera smoothing
	public float timer_x;

	public GameObject player;
	public GameObject floor;

	public float floorMargin; //Margin to adjust y axis on camera

	private PlayerMovement playerMovement;

	void Awake () {
		playerMovement = GetComponent<PlayerMovement> ();
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		//floor = GameObject.Find (playerMovement.floorTag.ToString());
		floor = playerMovement.floorTag;
	}

	void FixedUpdate () {

		float position_x = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, timer_x);
		float position_y = Mathf.SmoothDamp (transform.position.y, (floor.transform.position.y + floorMargin), ref velocity.y, timer_y);

		transform.position = new Vector3 (position_x, position_y, -1f);

	}

}