using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float ProjectileDamage;
	public float speed;
	public bool explode;
	public GameObject main_camera;
	public GameObject explosion;
	public GameObject Enemy;
	public string shooterID;
	private Transform target;

	private Vector2 targetPos;

	ParticleSystem[] childrenParticleSytems;

	void Awake () {
		childrenParticleSytems = gameObject.GetComponentsInChildren<ParticleSystem> ();
	}

	void Start () {
		main_camera = Camera.main.gameObject;

		if (shooterID == "FirstEnemy") {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetPos = new Vector2 (target.position.x, target.position.y);
		} else if ((shooterID == "Player_Rune_2") || (shooterID == "Player_Rune_5")) {
			targetPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, targetPos, speed * Time.deltaTime); //the projectile moves towards the targetPosition position of where the player was when it shot

		if ((transform.position.x == targetPos.x) && (transform.position.y == targetPos.y)) { //if the x and y coordinates are equal to the targets coordinates
			StartCoroutine (Countdown (1f, () => { DestroyProjectile (); })); //after 1 sec of hitting the desired pos, projectile deletes itself
			StartCoroutine (Countdown (0.1f, () => { //Stops looping of creation of particles allowing for them to stop being produced
				foreach (ParticleSystem childPS in childrenParticleSytems) { //accessing children of projectile (particle managers)
					var main = childPS.main;
					main.loop = false;
				}
			}));
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if ((shooterID == "FirstEnemy") && (other.CompareTag ("Player"))) {
			main_camera.GetComponent<PlayerManager> ().TakeDamage (ProjectileDamage);
		}
		if (((shooterID == "Player") || (shooterID == "Player_Rune_2") || (shooterID == "Player_Rune_5")) && (other.CompareTag ("Enemy_1"))) {
			targetPos = other.GetComponent<Transform> ().position;
			other.GetComponent<Firstenemy> ().TakeDamage (ProjectileDamage);
		}
		if ((!other.CompareTag ("Player")) && (shooterID == "Player")) {
			targetPos = other.GetComponent<Transform> ().position; //Stops the projectile when it collides with something on the way
		}
	}

	void DestroyProjectile () {
		Destroy (gameObject); //Destroys the projectile
	}
}