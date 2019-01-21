using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour {

	public bool CheckpointAllow;

	void OnDrawGizmos () { //So it works on editor when game is not running
		HelpArrowsController ();
	}

	void Start () {
		for (int a = 0; a < transform.childCount; a++) {
			for (int b = 0; b < transform.childCount; b++) {
				transform.GetChild (a).GetChild (b).gameObject.SetActive (false);
			}
		}
	}

	void Update () {
		HelpArrowsController ();
	}

	void HelpArrowsController () {
		foreach (Transform menuChild in transform) {
			if (GameManager.Instance.HelpArrowsEnabled == "OFF") {
				menuChild.gameObject.SetActive (false);
				CheckpointAllow = false;
			}
			if (GameManager.Instance.HelpArrowsEnabled == "AUTO") {
				if ((!menuChild.gameObject.activeSelf) && (CheckpointAllow)) {
					menuChild.gameObject.SetActive (true);
				}
			}
			if (GameManager.Instance.HelpArrowsEnabled == "ON") {
				menuChild.gameObject.SetActive (true);
				CheckpointAllow = false;
			}
		}
	}
}