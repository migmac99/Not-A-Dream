using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

	public float movementSpeed;

	// Use this for initialization
	void Start () {
		StartCoroutine (Countdown (20f, () => { Destroy (gameObject); }));
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.left * movementSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Destroy") {
			//Debug.Log("Hi");
			Destroy (gameObject);
		}
	}
}