using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Animator animator;

	public float ProjectileDamage;
	public float speed;
	public bool explode;
	public GameObject main_camera;
	public GameObject explosion;
	public GameObject Enemy;
	public string shooterID;
	private Transform target;

	private Vector2 targetPos;
	// Use this for initialization
	void Start () {
		animator = explosion.GetComponent<Animator> ();
		main_camera = Camera.main.gameObject;

		if (shooterID == "FirstEnemy") {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetPos = new Vector2 (target.position.x, target.position.y);
			animator.SetBool ("Green", true);
		} else if (shooterID == "Player") {
			targetPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			animator.SetBool ("Purple", true);
		}
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
		transform.position = Vector2.MoveTowards (transform.position, targetPos, speed * Time.deltaTime); //the projectile moves towards the targetPosition position of where the player was when it shot

		if ((transform.position.x == targetPos.x) && (transform.position.y == targetPos.y)) { //if the x and y coordinates are equal to the targets coordinates
			if (explode) {
				animator.SetBool ("Explode", true);
				StartCoroutine (Countdown (0.2f, () => { DestroyProjectile (); }));
			} else { //Gets rid of ghost trails
				StartCoroutine (Countdown (0.5f, () => { DestroyProjectile (); }));
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if ((shooterID == "FirstEnemy") && (other.CompareTag ("Player"))) {
			main_camera.GetComponent<PlayerManager> ().TakeDamage (ProjectileDamage);
		}

		if ((shooterID == "Player") && (other.CompareTag ("Enemy_1"))) {
			targetPos = other.GetComponent<Transform> ().position;

			//Firstenemy Enemy = GetComponent<Firstenemy>();
			//Enemy.TakeDamage (ProjectileDamage);

			other.GetComponent<Firstenemy> ().TakeDamage (ProjectileDamage);
			//Debug.Log();
		}

		if ((!other.CompareTag ("Player")) && (shooterID == "Player")) {
			targetPos = other.GetComponent<Transform> ().position; //Stops the projectile when it collides with something on the way
		}
	}

	void DestroyProjectile () {
		Destroy (gameObject); //Destroys the projectile
	}
}