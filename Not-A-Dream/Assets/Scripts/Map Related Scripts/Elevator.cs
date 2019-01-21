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

	void Awake () {
		animator = GetComponent<Animator> ();
		isOpen = false;
	}

	void Update () {
		if ((isOpen) && (Input.GetKey (KeyCode.W))) {
			//SceneManager.LoadScene ("not-a-dream");
			SceneManager.LoadScene (SelectedSceneString);
			GameManager.Instance.CurrentScene = SelectedSceneString;
			if (EnableCheckpoint) {
				GameManager.Instance.CurrentCheckpointPos = SelectedCheckpoint;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			animator.SetBool ("isOpen", true);
			isOpen = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			animator.SetBool ("isOpen", false);
			isOpen = false;
		}
	}
}