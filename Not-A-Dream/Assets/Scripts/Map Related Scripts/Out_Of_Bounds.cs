using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out_Of_Bounds : MonoBehaviour {

	public bool isLava;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (isLava) {
				var main_Camera = GameObject.FindGameObjectWithTag ("MainCamera");
				main_Camera.GetComponent<PlayerManager> ().TakeDamage (100);
			} else
				other.transform.position = GameManager.Instance.CurrentCheckpointPos;
		}
		other.GetComponent<RunePowers> ().Rune_1_CurrentTime = 0;
		other.GetComponent<RunePowers> ().Rune_2_CurrentTime = 0;
		other.GetComponent<RunePowers> ().Rune_3_CurrentTime = 0;
		other.GetComponent<RunePowers> ().Rune_4_CurrentTime = 0;
		other.GetComponent<RunePowers> ().Rune_5_CurrentTime = 0;
	}
}