using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	[Range (0, 50)]
	public int SecondsBeforeArrowShown;
	[Space (10)]
	public GameObject AssignedArrowController;
	[Space (20)]
	public GameObject main_camera;
	[Space (10)]
	public Path_Manager DesiredDirectorPath;

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	void Awake () {
		GetComponent<SpriteRenderer> ().color = GameManager.Instance.CheckpointColor;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (!GetComponent<Animator> ().GetBool ("Active")) {
				//Director ();
				GetComponent<Animator> ().SetBool ("Active", true);
				GameManager.Instance.CurrentCheckpointPos = transform.position;
				if (AssignedArrowController != null) {
					StartCoroutine (Countdown (SecondsBeforeArrowShown, () => { AssignedArrowController.GetComponent<ArrowControl> ().CheckpointAllow = true; }));
				}
			}

		}
	}

	void Director () {
		//Debug.Log (DesiredDirectorPath.path_objs.Count);
		main_camera.GetComponent<CameraMovement> ().DirectorPath = DesiredDirectorPath;
		main_camera.GetComponent<CameraMovement> ().DirectorMode = true;
		StartCoroutine (Countdown (GameManager.Instance.DirectorCutTime * DesiredDirectorPath.path_objs.Count, () => { main_camera.GetComponent<CameraMovement> ().DirectorMode = false; }));
	}
}