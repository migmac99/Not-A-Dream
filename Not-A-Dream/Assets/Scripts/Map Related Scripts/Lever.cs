using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour {

	private Animator animator;
	public bool LeverStatus;
	public string TargetMode;
	[Space (10)]
	public GameObject SelectedPlatform;
	[Space (10)]
	[Range (0, 20)]
	public int leverTimer;
	private bool Disable;
	[Space (20)]
	public GameObject main_camera;
	[Space (10)]
	public Path_Manager DesiredDirectorPath;
	[Space (10)]
	private bool hasRun = false;

	void Awake () {
		animator = GetComponent<Animator> ();
		Disable = false;
		hasRun = false;
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	void Update () {
		if (Disable) {
			Disable = false;
			StartCoroutine (Countdown (leverTimer, () => { animator.SetBool ("On", false); LeverStatus = false; }));
		}

		if (TargetMode == "Disable") {
			for (int a = 0; a < transform.childCount; a++) {
				if (LeverStatus) {
					transform.GetChild (a).gameObject.SetActive (false);
					Director ();
				} else {
					transform.GetChild (a).gameObject.SetActive (true);
				}
			}
		}

		if (TargetMode == "Enable_Platform") {
			if (LeverStatus) {
				SelectedPlatform.GetComponent<ParticleFollowing> ().PlatformEnabled = true;
				Director ();
			} else {
				SelectedPlatform.GetComponent<ParticleFollowing> ().PlatformEnabled = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			animator.SetBool ("On", true);
			LeverStatus = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (TargetMode == "Disable") {
			if ((other.CompareTag ("Player")) && (leverTimer != 0)) {
				Disable = true;
			}
		}
	}

	void Director () {
		if (!hasRun) {
			main_camera.GetComponent<CameraMovement> ().Player.GetComponent<PlayerMovement> ().PlayerPaused = true;
			main_camera.GetComponent<CameraMovement> ().DirectorPath = DesiredDirectorPath;
			main_camera.GetComponent<CameraMovement> ().DirectorMode = true;
			StartCoroutine (Countdown (GameManager.Instance.DirectorCutTime * DesiredDirectorPath.path_objs.Count, () => { main_camera.GetComponent<CameraMovement> ().DirectorMode = false; main_camera.GetComponent<CameraMovement> ().Player.GetComponent<PlayerMovement> ().PlayerPaused = false; }));
			hasRun = true;
		}
	}
}