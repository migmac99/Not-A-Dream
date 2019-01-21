using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Projectile")) {
			for (int a = 0; a < transform.childCount; a++) {
				transform.GetChild (a).gameObject.SetActive (true);
				Debug.Log ("CHILD " + a);
			}
			Debug.Log ("COLLIDED");
			//	gameObject.SetActive(true);
		}
		Debug.Log ("ANYTHING");
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("ANYTHING_TRIGGER");
	}
}