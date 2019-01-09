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
	public string shooterID;
	private Transform shooter;

	private Vector2 target;
	// Use this for initialization
	void Start () {
		animator = explosion.GetComponent<Animator> ();
		main_camera = Camera.main.gameObject;

		if (shooterID == "FirstEnemy") {
			shooter = GameObject.FindGameObjectWithTag ("Player").transform;
			target = new Vector2 (shooter.position.x, shooter.position.y);
			animator.SetBool ("Green", true);
		} else if (shooterID == "Rune_2"){
			target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
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
		transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime); //the projectile moves towards the target position of where the player was when it shot

		if ((transform.position.x == target.x) && (transform.position.y == target.y)) { //if the x and y coordinates are equal to the targets coordinates
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
			main_camera.GetComponent<GameManager> ().TakeDamage (ProjectileDamage);
			if (explode) {
				animator.SetBool ("Explode", true);
				StartCoroutine (Countdown (0.3f, () => { DestroyProjectile (); }));
			} else {
				StartCoroutine (Countdown (0.7f, () => { DestroyProjectile (); }));
			}
		}
	}

	void DestroyProjectile () {
		Destroy (gameObject); //Destroys the projectile
	}
}