using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firstenemy : MonoBehaviour {

	private Rigidbody2D rb;

	public float Attack_1_damage; //Damage value for Attack_1 (this controlls other scripts)
	public float Attack_2_damage; //Damage value for Attack_2 (this controlls other scripts)

	public string State = "Idle"; //Various states that will decide on what the enemy will do
	public string Fight_State; //State of the fight
	public string Attack_State; //State of the attack

	private Vector2 velocity; //Required for smoothing calculations

	private float timeBtwShots; //Time between Attack_1
	public float startTimeBtwShots;
	public float ghost_creation_timer; //Time the Attack_1 creates projectile ghosts (trail) for

	public float timer_x; //Timer for smoothing
	public float timer_y;
	public float xOffset; //Offset to adjust x axis
	public float arrow_offset_y; //Offset for Arrow during Attack_2
	public float target_offset_y; //Offset for Target during Attack_2
	public float step_back_value_x; //Value for the step back after Attack_2 is done
	public float enemy_step_back_timer_x; //Timer for the step back after Attack_2 is done
	public float enemy_timer_x;

	public float dist; //Distance between the player and the enemy (positive if player is on the right of enemy and negative)
	private float position_x_enemy;
	private float position_y_enemy;

	public float Jump_Force; //Jump Force

	public float enemySpeed; //Enemies speed
	public float rayDistance; //Raycasts distance
	public float startingDistance; //How far the enemy stops from the player

	public Sprite enemyAttackSprite;

	private bool movingRight = true; //tells the character where to go when reaches the edge of the platform
	public Transform groundDetect; //Empty object from where the raycast is shot

	public bool isTouching_Player; //Check if enemy is touching the player
	public bool isTouching_Floor; //Check if enemy is touching the floor

	public GameObject projectile;
	public GameObject desiredPos;
	public GameObject player;
	public GameObject main_camera;
	public GameObject floor;
	public GameObject arrow;
	public GameObject target;

	public SpriteRenderer arrow_spriteRenderer;
	public SpriteRenderer target_spriteRenderer;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		timeBtwShots = startTimeBtwShots;

		arrow_spriteRenderer = arrow.GetComponent<SpriteRenderer> ();
		target_spriteRenderer = target.GetComponent<SpriteRenderer> ();

		arrow_spriteRenderer.enabled = false;
		target_spriteRenderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (State == "Idle") {
			Idle ();
		} else if (State == "Fight") {
			Fight ();
		}
		if (Fight_State == "Attack_1") {
			Attack_1 ();
		}
		if (Fight_State == "Attack_2") {
			Attack_2 ();
		}

		if (Vector2.Distance (transform.position, player.transform.position) <= startingDistance) {
			State = "Fight";
			this.GetComponent<SpriteRenderer> ().sprite = enemyAttackSprite;
		}
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	//Check if the enemy is touching the floor/player
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == player) {
			isTouching_Player = true;
		}
		if (other.gameObject == floor) {
			isTouching_Floor = true;
		}
	}

	//Check if the enemy has stopped touching the floor/player
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject == player) {
			isTouching_Player = false;
		}
		if (other.gameObject == floor) {
			isTouching_Floor = false;
		}
	}

	void Idle () {
		transform.Translate (Vector2.right * enemySpeed * Time.deltaTime); // this makes the enemy move according to the vector2's x - and + axis
		RaycastHit2D groundInfo = Physics2D.Raycast (groundDetect.position, Vector2.down, rayDistance); //Shoots out a ray that detects if there is a floor, first variable is the object its shooting from, the second is in which direction and third is how long the ray is.

		if (!groundInfo.collider) //If the collider has not detected with the floor
		{
			if (movingRight) {
				transform.eulerAngles = new Vector3 (0, -180, 0); //Changing direction to left
				movingRight = false;
			} else {
				transform.eulerAngles = new Vector3 (0, 0, 0); //Changing direction to right
				movingRight = true;
			}
		}
	}

	void Fight () {
		if ((Fight_State != "Attack_1") && (Fight_State != "Attack_2")) { //Moving enemy towards player when enemy is not attacking and player is too far away
			Follow_Player ();
		}
		if ((timeBtwShots <= 0) && (Fight_State != "Attack_2")) {
			Fight_State = "Attack_1";
			Attack_State = "In_Progress";
		} else {
			timeBtwShots -= Time.deltaTime;
		}
		if ((Input.GetKey (KeyCode.Q)) && (Fight_State != "Attack_2")) {
			Fight_State = "Attack_2";
			Attack_State = "In_Progress";
		}

	}

	void Follow_Player () {
		dist = player.transform.position.x - transform.position.x;

		float position_x = Mathf.SmoothDamp (transform.position.x, desiredPos.transform.position.x + xOffset, ref velocity.x, timer_x);

		if (dist > 0) { //If player is to the right, enemy looks right
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else if (dist < 0) { //If player is to the left, enemy looks left
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}
		if ((dist > 6) && (player.GetComponent<PlayerMovement> ().Speed > 0)) { //If player is too far away and is going to the right
			xOffset = -Math.Abs (xOffset);
			transform.position = new Vector3 (position_x, transform.position.y, 0); //Enemy follows player
		} else if ((dist < -6) && (player.GetComponent<PlayerMovement> ().Speed < 0)) { //If player is too far away and is going to the left
			xOffset = Math.Abs (xOffset);
			transform.position = new Vector3 (position_x, transform.position.y, 0); //Enemy follows player
		}
	}

	void Attack_1 () {
		if (Attack_State == "In_Progress") {
			var Projectile_Instance = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
			Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = Attack_1_damage;
			Projectile_Instance.GetComponent<Projectile> ().explode = true;
			timeBtwShots = startTimeBtwShots;
			Attack_State = "Attack_Ghosts";
		}
		if (Attack_State == "Attack_Ghosts") {
			var Projectile_Instance = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
			Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = 0;
			Projectile_Instance.GetComponent<Projectile> ().explode = false;
			timeBtwShots = startTimeBtwShots;
			StartCoroutine (Countdown (ghost_creation_timer, () => { Attack_State = "Attack_Finish"; }));
		}
		if (Attack_State == "Attack_Finish") {
			Fight_State = "";
			Attack_State = "";
		}
	}

	void Attack_2 () {
		if (Attack_State == "In_Progress") {
			StartCoroutine (Countdown (0.5f, () => { Attack_State = "Mid_Air"; }));
			Attack_State = "Jumping";
		}
		if (Attack_State == "Jumping") {
			rb.velocity = Vector2.up * Jump_Force;
		}
		if (Attack_State == "Mid_Air") {
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
			Attack_State = "Move_x_to_player";
			StartCoroutine (Countdown (6f, () => { Attack_State = "Drop_from_sky"; }));
		}
		if (Attack_State == "Move_x_to_player") {
			arrow_spriteRenderer.enabled = true;
			target_spriteRenderer.enabled = true;

			//Arrow Movement
			float position_x_arrow = Mathf.SmoothDamp (arrow.transform.position.x, transform.position.x, ref velocity.x, 0);
			float position_y_arrow = Mathf.SmoothDamp (arrow.transform.position.y, main_camera.transform.position.y + arrow_offset_y, ref velocity.y, 0);
			arrow.transform.position = new Vector3 (position_x_arrow, position_y_arrow, 0);

			//Target Movement
			float position_y_target = Mathf.SmoothDamp (target.transform.position.y, floor.transform.position.y + target_offset_y, ref velocity.y, 0);
			target.transform.position = new Vector3 (position_x_arrow, position_y_target, 0);

			//Enemy Movement
			position_x_enemy = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, enemy_timer_x);
			position_y_enemy = Mathf.SmoothDamp (transform.position.y, transform.position.y, ref velocity.y, 0);
			transform.position = new Vector3 (position_x_enemy, position_y_enemy, 0);
		}
		if (Attack_State == "Drop_from_sky") {
			rb.gravityScale = 1;
			arrow_spriteRenderer.enabled = false;

			float position_x_target = Mathf.SmoothDamp (transform.position.x, transform.position.x, ref velocity.x, 0);
			float position_y_target = Mathf.SmoothDamp (target.transform.position.y, floor.transform.position.y + target_offset_y, ref velocity.y, 0);
			target.transform.position = new Vector3 (position_x_target, position_y_target, 0);

			if ((isTouching_Floor) || (isTouching_Player)) {
				Attack_State = "Step_Back";
				StartCoroutine (Countdown (2f, () => { Attack_State = "Attack_Finish"; }));
				if (isTouching_Player) {
					main_camera.GetComponent<GameManager> ().TakeDamage (Attack_2_damage);
					Attack_State = "Step_Back";
				}
			}
		}
		if (Attack_State == "Step_Back") {
			target_spriteRenderer.enabled = false;
			dist = player.transform.position.x - transform.position.x;

			if (dist > 0) { //If player is to the right of enemy
				position_x_enemy = Mathf.SmoothDamp (transform.position.x, transform.position.x - step_back_value_x, ref velocity.x, enemy_step_back_timer_x);
				position_y_enemy = Mathf.SmoothDamp (transform.position.y, transform.position.y, ref velocity.y, 0);
			} else if (dist < 0) { //If player is to the left of enemy
				position_x_enemy = Mathf.SmoothDamp (transform.position.x, transform.position.x + step_back_value_x, ref velocity.x, enemy_step_back_timer_x);
				position_y_enemy = Mathf.SmoothDamp (transform.position.y, transform.position.y, ref velocity.y, 0);
			}
			transform.position = new Vector3 (position_x_enemy, position_y_enemy, 0);
		}
		if (Attack_State == "Attack_Finish") {
			Fight_State = "";
			Attack_State = "";
			position_x_enemy = 0;
			position_y_enemy = 0;
		}
	}
}

////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////	TO DO	////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
////																					////
////   [x]	 Add the arrow following the x of the enemy and the y of the camera			////
////   [x]	 		Make enemy follow the player while in move_to_player				////
////   [x]			      Add timer to change from following player to land				////
////   [x]							Add sprite for target area							////
////   [x]						After landing do damage to player						////
////   [x]		After damaging the player, enemy goes back a few "steps"?				////
////   [x]	Step back to adequate position left or right depending on player pos		////
////   []						Refine timings for good fight							////
////																					////
////////////////////////////////////////////////////////////////////////////////////////////