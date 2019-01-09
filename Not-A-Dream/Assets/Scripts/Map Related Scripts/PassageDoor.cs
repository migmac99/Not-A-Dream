using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageDoor : MonoBehaviour {

	[Header ("╔═══════════════[Referencing]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject player;

	void Update () {
		if (player.GetComponent<RunePowers> ().Rune_3_State == "In_Progress") {
			GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			GetComponent<BoxCollider2D> ().enabled = true;
		}
	}
}