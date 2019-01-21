using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColliderDamager : MonoBehaviour {

	public GameObject player;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Enemy_1")) {
			other.GetComponent<Firstenemy> ().TakeDamage (player.GetComponent<RunePowers> ().Rune_4_Damage);
		}
	}
}