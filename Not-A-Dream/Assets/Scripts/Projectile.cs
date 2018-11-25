using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour {

	private Animator animator;

	public float ProjectileDamage;
	public float speed;
	public bool explode;
	public GameObject main_camera;
	public GameObject explosion;

	private Transform player;
	private Vector2 target;
	// Use this for initialization
	void Start () {
		animator = explosion.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		main_camera = Camera.main.gameObject;
		target = new Vector2 (player.position.x, player.position.y);
	}

	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime); //the projectile moves towards the target position of where the player was when it shot

		if ((explode) && (transform.position.x == target.x) && (transform.position.y == target.y)) { //if the x and y coordinates are equal to the targets coordinates
			animator.SetBool("Explode", true);
			StartCoroutine (Countdown (0.3f, () => {DestroyProjectile();}));
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			main_camera.GetComponent<GameManager> ().TakeDamage(ProjectileDamage);
			if (explode){
				animator.SetBool("Explode", true);
				StartCoroutine (Countdown (0.3f, () => {DestroyProjectile();}));
			}else{
				StartCoroutine (Countdown (0.75f, () => {DestroyProjectile();}));
			}
		}
	}

	void DestroyProjectile () {
		Destroy (gameObject); //Destroys the projectile
	}
}