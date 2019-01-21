using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawn : MonoBehaviour {
	public GameObject projectile;
	public string State;
	public float TimeBetweenBoulders;

	// Use this for initialization
	void Start () {
		State = "Fire";
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
		if (State == "Load") {
			State = "Waiting";
			StartCoroutine (Countdown (TimeBetweenBoulders, () => { State = "Fire"; }));
		}

		if (State == "Fire") {
			//var Projectile_Instance = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity);
			State = "Load";
		}
	}
}