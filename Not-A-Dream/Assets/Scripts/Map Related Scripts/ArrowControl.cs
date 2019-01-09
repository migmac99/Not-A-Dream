using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour {

	void OnDrawGizmos () { //So it works on editor when game is not running
		HelpArrowsController ();
	}

	void Update () {
		HelpArrowsController ();
	}

	void HelpArrowsController () {
		foreach (Transform menuChild in transform) {
			//Debug.Log ("child" + menuChild.name);
			if (GameManager.Instance.HelpArrowsEnabled == "OFF") {
				menuChild.gameObject.SetActive (false);
			}
			if (GameManager.Instance.HelpArrowsEnabled == "AUTO") {
				menuChild.gameObject.SetActive (true);
			}
			if (GameManager.Instance.HelpArrowsEnabled == "ON") {
				menuChild.gameObject.SetActive (true);
			}
		}
	}
}