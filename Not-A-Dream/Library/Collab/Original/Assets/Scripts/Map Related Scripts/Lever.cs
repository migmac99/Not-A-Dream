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

	void Awake () {
		animator = GetComponent<Animator> ();
		Disable = false;
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
				} else {
					transform.GetChild (a).gameObject.SetActive (true);
				}
			}
		}

		if (TargetMode == "Enable_Platform") {
			if (LeverStatus) {
				SelectedPlatform.GetComponent<ParticleFollowing> ().PlatformEnabled = true;
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
}