using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firstenemy : MonoBehaviour {

	private Rigidbody2D rb;
	private Vector2 velocity; //Required for smoothing calculations

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	[Header ("╔═══════════════[Referencing]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public Sprite enemyDefaultSprite;
	public Sprite enemyAttackSprite;
	[Space (10)]
	public GameObject player;
	public GameObject main_camera;
	[Space (10)]
	public GameObject Sky_Limit;
	public GameObject floor;
	public Transform groundDetect; //Empty object from where the raycast is shot
	[Space (10)]
	public GameObject arrow;
	public GameObject target;
	[Space (10)]
	public GameObject projectile;
	public GameObject desiredPos;
	[Space (10)]
	public SpriteRenderer arrow_spriteRenderer;
	public SpriteRenderer target_spriteRenderer;
	[Space (10)]
	public FadeOutGameObject _FadeOutGameObject;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═══════════════[Arena]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject wall_left;
	[Space (10)]
	public GameObject wall_right;
	[Space (10)]
	public GameObject defaultArenaPos;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Attack Settings]════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	[Range (0, 20)] public int Attack_1_Min_Frequency;
	[Range (0, 20)] public int Attack_1_Max_Frequency;
	[Space (10)]
	[Range (0, 20)] public int Attack_2_Min_Frequency;
	[Range (0, 20)] public int Attack_2_Max_Frequency;
	[Space (10)]

	[HideInInspector]
	public bool hasChosenTime_1 = false;
	[HideInInspector]
	public bool hasChosenTime_2 = false;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Attack Damage]════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	[Range (0, 100)] public float Attack_1_damage; //Damage value for Attack_1 (this controlls other scripts)
	[Range (0, 100)] public float Attack_2_damage; //Damage value for Attack_2 (this controlls other scripts)

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Enemy Health]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject HealthBar_all;
	[Space (5)]
	public Image healthbar;
	[Space (5)]
	public float EnemyHealth;
	[Space (5)]
	public float idleRegenAmmount;

	//[HideInInspector]
	public bool RegenActive;

	[HideInInspector] //Doesnt show in inspector
	public float StartEnemyHealth; //Static value for healthbar math

	[HideInInspector]
	public bool isDead; //for the death to only occur once;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Enemy States]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public string State = "Idle"; //Various states that will decide on what the enemy will do
	public string Fight_State; //State of the fight
	public string Attack_State; //State of the attack

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Is Touching]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public bool isTouching_Player; //Check if enemy is touching the player
	public bool isTouching_Floor; //Check if enemy is touching the floor
	public bool isTouching_Arena_Wall_L; //Check if enemy is touching Arena wall Left side
	public bool isTouching_Arena_Wall_R; //Check if enemy is touching Arena wall Right side

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Follow Player]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float xOffset; //Offset to adjust x axis

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Other Movement]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float dist; //Distance between the player and the enemy (positive if player is on the right of enemy and negative)
	[Space (10)]
	public float startingDistance; //How far the enemy is from the player for (State = "Attack")

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Attack 1]═══════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float startTimeBtwShots_1; //Value for timer
	private float timeBtwShots_1; //Current Timer of time between Attack_1

	public float ghost_creation_timer; //Time the Attack_1 creates projectile ghosts (trail) for	

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Attack 2]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float startTimeBtwShots_2; //Value for timer
	private float timeBtwShots_2; //Current Timer of time between Attack_2
	[Space (10)]
	public float timer_x; //Timer for smoothing
	[Space (10)]
	public float arrow_offset_y; //Offset for Arrow during Attack_2
	public float target_offset_y; //Offset for Target during Attack_2
	[Space (10)]
	public float step_back_value_x; //Value for the step back after Attack_2 is done
	public float enemy_step_back_timer_x; //Timer for the step back after Attack_2 is done
	[Space (10)]
	public float enemy_timer_x;
	[Space (10)]
	public float Jump_Force; //Jump Force

	private float position_x_enemy;
	private float position_y_enemy;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Idle]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	[HideInInspector]
	public RaycastHit2D groundInfo;

	public float enemySpeed; //Enemies speed
	public float rayDistance; //Raycasts distance

	//private bool movingRight = true; //tells the character where to go when reaches the edge of the platform

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		timeBtwShots_1 = startTimeBtwShots_1;

		arrow_spriteRenderer = arrow.GetComponent<SpriteRenderer> ();
		target_spriteRenderer = target.GetComponent<SpriteRenderer> ();

		arrow_spriteRenderer.enabled = false;
		target_spriteRenderer.enabled = false;

		Physics2D.IgnoreCollision (Sky_Limit.GetComponent<BoxCollider2D> (), GetComponent<BoxCollider2D> ()); //Prevents the enemy from colliding with the sky limit (this allows Attack_2 to work propperly)
	}

	void Start () {
		StartEnemyHealth = EnemyHealth;

		defaultArenaPos.transform.position = new Vector3 (transform.position.x, 50, 0);
	}

	// Update is called once per frame
	void Update () {
		groundInfo = Physics2D.Raycast (groundDetect.position, Vector2.down, rayDistance); //Shoots out a ray that detects if there is a floor, first variable is the object its shooting from, the second is in which direction and third is how long the ray is.

		if (State == "Idle") {
			Idle ();
			GetComponent<SpriteRenderer> ().sprite = enemyDefaultSprite;
		} else if (State == "Fight") {
			Fight (UnityEngine.Random.Range (Attack_1_Min_Frequency, Attack_1_Max_Frequency), UnityEngine.Random.Range (Attack_2_Min_Frequency, Attack_2_Max_Frequency));
			if ((isTouching_Arena_Wall_L) || (isTouching_Arena_Wall_R)) {
				if (Fight_State == "Attack_2") {
					if (Attack_State == "Move_x_to_player") {
						Attack_State = "Move_x_to_default_pos";
					}
				} else {
					State = "Idle";
				}
			}
		} else if (State == "Die") {
			Die ();
		}

		if (Fight_State == "Attack_1") {
			Attack_1 ();
		} else if (Fight_State == "Attack_2") {
			Attack_2 ();
		}

		if (Vector2.Distance (transform.position, player.transform.position) <= startingDistance) {
			State = "Fight";
			GetComponent<SpriteRenderer> ().sprite = enemyAttackSprite;
		}

		Health ();
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		yield return new WaitForSecondsRealtime (seconds);
		onComplete ();
	}

	//Check if the enemy is touching the floor/player/arena_walls
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == player) {
			isTouching_Player = true;
		}
		if (other.gameObject == floor) {
			isTouching_Floor = true;
		}

		if (other.gameObject.CompareTag ("Arena_Wall_L")) { //This makes sure the enemy never leaves the arena
			isTouching_Arena_Wall_L = true;
		}
		if (other.gameObject.CompareTag ("Arena_Wall_R")) {
			isTouching_Arena_Wall_R = true;
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
		if (other.gameObject.CompareTag ("Arena_Wall_L")) {
			isTouching_Arena_Wall_L = false;
		}
		if (other.gameObject.CompareTag ("Arena_Wall_R")) {
			isTouching_Arena_Wall_R = false;
		}
	}

	void Idle () {
		Fight_State = "";
		Attack_State = "";

		transform.Translate (Vector2.right * enemySpeed * Time.deltaTime); // this makes the enemy move according to the vector2's x - and + axis

		Regen (idleRegenAmmount);

		if (isTouching_Arena_Wall_L) { //If the collider has not detected with the floor
			transform.eulerAngles = new Vector3 (0, 0, 0); //Changing Enemy direction to right
			healthbar.transform.eulerAngles = new Vector3 (0, 0, 0); //Makes the healthbar remain in the same orientation
		}
		if (isTouching_Arena_Wall_R) {
			transform.eulerAngles = new Vector3 (0, -180, 0); //Changing Enemy direction to left
			healthbar.transform.eulerAngles = new Vector3 (0, 0, 0); //Makes the healthbar remain in the same orientation
		}
	}

	void Fight (int Attack_1_Timing = 1, int Attack_2_Timing = 1) {
		if ((Fight_State != "Attack_1") && (Fight_State != "Attack_2")) { //Moving enemy towards player when enemy is not attacking and player is too far away
			if (isTouching_Floor) {
				Follow_Player ();
			}
		}

		if ((Attack_State == "Attack_Finish") && (Fight_State == "")) {
			Fight_State = "";
			Attack_State = "";
		}

		if ((!hasChosenTime_1) && (Fight_State == "") && (Attack_State == "")) {
			startTimeBtwShots_1 = Attack_1_Timing;
			hasChosenTime_1 = true;
		} else {
			if ((timeBtwShots_1 <= 0) && (Fight_State == "") && (Attack_State == "")) {
				Fight_State = "Attack_1";
				Attack_State = "In_Progress";
				hasChosenTime_1 = false;
			} else {
				timeBtwShots_1 -= Time.deltaTime;
			}
		}

		if ((!hasChosenTime_2) && (Fight_State == "") && (Attack_State == "")) {
			timeBtwShots_2 = Attack_2_Timing;
			hasChosenTime_2 = true;
		} else {
			if ((timeBtwShots_2 <= 0) && (Fight_State == "") && (Attack_State == "")) {
				Fight_State = "Attack_2";
				Attack_State = "In_Progress";
				hasChosenTime_2 = false;
			} else {
				timeBtwShots_2 -= Time.deltaTime;
			}
		}
	}

	void Follow_Player () {
		dist = player.transform.position.x - transform.position.x;

		float position_x = Mathf.SmoothDamp (transform.position.x, desiredPos.transform.position.x + xOffset, ref velocity.x, timer_x);

		if (dist > 0) { //If player is to the right, enemy looks right
			transform.eulerAngles = new Vector3 (0, 0, 0);
			healthbar.transform.eulerAngles = new Vector3 (0, 0, 0); //Makes the healthbar remain in the same orientation
		} else if (dist < 0) { //If player is to the left, enemy looks left
			transform.eulerAngles = new Vector3 (0, 180, 0);
			healthbar.transform.eulerAngles = new Vector3 (0, 0, 0); //Makes the healthbar remain in the same orientation
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
		// if (Attack_State == "In_Progress") {
		// 	var Projectile_Instance = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
		// 	Projectile_Instance.GetComponent<Projectile> ().shooterID = "FirstEnemy";
		// 	Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = Attack_1_damage;
		// 	Projectile_Instance.GetComponent<Projectile> ().explode = true;
		// 	timeBtwShots_1 = startTimeBtwShots_1;
		// 	Attack_State = "Attack_Ghosts";
		// }
		// if (Attack_State == "Attack_Ghosts") {
		// 	var Projectile_Instance = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
		// 	Projectile_Instance.GetComponent<Projectile> ().shooterID = "FirstEnemy";
		// 	Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = 0;
		// 	Projectile_Instance.GetComponent<Projectile> ().explode = false;
		// 	timeBtwShots_1 = startTimeBtwShots_1;
		// 	StartCoroutine (Countdown (ghost_creation_timer, () => { Attack_State = "Attack_Finish"; }));
		//}

		if (Attack_State == "In_Progress") {
			var Projectile_Instance = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
			Projectile_Instance.GetComponent<Projectile> ().shooterID = "FirstEnemy";
			Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = Attack_1_damage;
			timeBtwShots_1 = startTimeBtwShots_1;
			Attack_State = "Attack_Finish";
		}

		if (Attack_State == "Attack_Finish") {
			Fight_State = "";
			Attack_State = "";
		}
	}

	void Attack_2 () {
		if (Attack_State == "In_Progress") {
			StartCoroutine (Countdown (1f, () => { Attack_State = "Mid_Air"; }));
			Attack_State = "Jumping";
		}
		if (Attack_State == "Jumping") {
			rb.velocity = Vector2.up * Jump_Force;
		}
		if (Attack_State == "Mid_Air") {
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
			Attack_State = "Move_x_to_player";
			StartCoroutine (Countdown (6f, () => { if (Attack_State == "Move_x_to_player") { Attack_State = "Drop_from_sky"; } }));
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
		if (Attack_State == "Move_x_to_default_pos") {
			//Enemy Movement
			position_x_enemy = Mathf.SmoothDamp (transform.position.x, defaultArenaPos.transform.position.x, ref velocity.x, enemy_timer_x);
			position_y_enemy = Mathf.SmoothDamp (transform.position.y, defaultArenaPos.transform.position.y, ref velocity.y, 0);
			transform.position = new Vector3 (position_x_enemy, position_y_enemy, 0);

			rb.gravityScale = 1;
			arrow_spriteRenderer.enabled = false;
			target_spriteRenderer.enabled = false;

			StartCoroutine (Countdown (0.5f, () => { Attack_State = "Attack_Finish"; State = "Idle"; }));
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
					main_camera.GetComponent<PlayerManager> ().TakeDamage (Attack_2_damage);
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

	void Health () {
		if (EnemyHealth <= 0) {
			State = "Die";
		}

		//Health Bar
		if (GameManager.Instance.HealthBarEnabled) {
			HealthBar_all.SetActive (true);
		} else {
			HealthBar_all.SetActive (false);
		}

		healthbar.fillAmount = EnemyHealth / StartEnemyHealth;

	}

	public void TakeDamage (float ammount) {
		if (EnemyHealth > 0) {
			EnemyHealth -= ammount;
		}
	}

	public void Regen (float ammount) { //This runs 1 time per second
		if (!RegenActive) {
			if (EnemyHealth < StartEnemyHealth) {
				RegenActive = true;
				EnemyHealth += ammount;
				StartCoroutine (Countdown (1f, () => { RegenActive = false; }));
			}
		}
	}

	void Die () {
		//Play death animation
		if (!isDead) {
			GetComponent<Collider2D> ().enabled = false;
			rb.isKinematic = true;
			_FadeOutGameObject.StartFading (0.025f);
			isDead = true;
		} else {
			//Replace 3f with time of death animation
			StartCoroutine (Countdown (3f, () => { Destroy (gameObject); }));
		}
	}
}