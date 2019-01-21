using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (GameObject.FindWithTag ("Projectile")) {
			for (int a = 0; a < transform.childCount; a++) {
				if (transform.GetChild (a).gameObject.activeSelf) {
					transform.GetChild (a).gameObject.SetActive (false);
				} else {
					transform.GetChild (a).gameObject.SetActive (true);
				}
			}
		}
	}
}