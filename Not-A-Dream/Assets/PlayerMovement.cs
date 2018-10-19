using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float xSpeed = 0f;
	public float ySpeed = 0f;
	Boolean TouchingFloor;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		KeyboardInput ();

		print (TouchingFloor);
	}

	void KeyboardInput () {

		if (Input.GetKey (KeyCode.D)) {
			this.transform.position += new Vector3 (xSpeed, ySpeed, 0);
			xSpeed = 0.01f;
			ySpeed = 0;
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.position += new Vector3 (xSpeed, ySpeed, 0);
			xSpeed = -0.01f;
			ySpeed = 0;

		}
		if (Input.GetKey (KeyCode.S)) {
			// this.transform.position += new Vector3(xSpeed, ySpeed, 0);
			// xSpeed = 0;
			// ySpeed = -0.05f;

		}
		if (TouchingFloor) {
			if (Input.GetKey (KeyCode.W)) {
				{
					this.transform.position += new Vector3 (xSpeed, ySpeed, 0);
					xSpeed = 0;
					ySpeed = 0.5f;

				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		TouchingFloor = true;
	}

	void OnTriggerExit2D (Collider2D other) {
		TouchingFloor = false;
	}

}