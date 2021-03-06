﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ultimate_Puzzle : MonoBehaviour {
	public GameObject doorClose;
	public GameObject doorOpen;
	public GameObject lights;
	//	private Animator animator;
	public bool doorState = false;
	public bool leftTrigger = true;
	Light lights_Light;

	[Space (20)]
	public GameObject main_camera;
	[Space (10)]
	public Path_Manager DesiredDirectorPath;

	// Use this for initialization
	void Start () {
		//	animator = GetComponent<Animator> ();
		lights_Light = lights.GetComponent<Light> ();
	}

	// Update is called once per frame
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if ((doorState) && (leftTrigger)) {
			lights_Light.intensity = 0.25f;
			doorClose.SetActive (false);
			doorOpen.SetActive (true);
			StartCoroutine (Countdown (0.1f, () => { doorState = false; Director (); }));
			//	StartCoroutine (Countdown (0.1f, () => { animator.SetBool ("On", false); }));
		}
		if ((other.CompareTag ("Explosion")) && (!doorState)) {
			//	animator.SetBool ("On", true);
			lights_Light.intensity = 2f;
			doorClose.SetActive (true);
			doorOpen.SetActive (false);
			doorState = true;
			leftTrigger = false;
			Director ();
		}

	}

	void OnTriggerExit2D (Collider2D other) {
		if ((other.CompareTag ("Explosion")) && (doorState)) {
			leftTrigger = true;
		}
	}

	void Director () {
		main_camera.GetComponent<CameraMovement> ().Player.GetComponent<PlayerMovement> ().PlayerPaused = true;
		main_camera.GetComponent<CameraMovement> ().DirectorPath = DesiredDirectorPath;
		main_camera.GetComponent<CameraMovement> ().DirectorMode = true;
		StartCoroutine (Countdown (GameManager.Instance.DirectorCutTime * DesiredDirectorPath.path_objs.Count, () => { main_camera.GetComponent<CameraMovement> ().DirectorMode = false; }));
	}
}