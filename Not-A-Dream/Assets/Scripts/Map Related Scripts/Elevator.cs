using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

	private Animator animator;
	private bool isOpen;

	public string SelectedSceneString;
	public bool EnableCheckpoint;
	public Vector2 SelectedCheckpoint;

	public int ThisElevatorNum;

	void Awake () {
		animator = GetComponent<Animator> ();
		isOpen = false;
	}

	void Start () {
		if (ThisElevatorNum != 0) {
			GameManager.Instance.UnlockedElevator[ThisElevatorNum] = true;
		}
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	void Update () {
		if ((isOpen) && (Input.GetKey (KeyCode.W))) {
			//SceneManager.LoadScene ("not-a-dream");
			SceneManager.LoadScene (SelectedSceneString);
			if (EnableCheckpoint) {
				GameManager.Instance.CurrentScene = SelectedSceneString;
				GameManager.Instance.CurrentCheckpointPos = SelectedCheckpoint;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			animator.SetBool ("isOpen", true);
			StartCoroutine (Countdown (0.75f, () => { isOpen = true; }));
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			animator.SetBool ("isOpen", false);
			isOpen = false;
		}
	}
}