using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RunePowers : MonoBehaviour {

	public Rigidbody2D rb;
	private Animator animator;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	[Header ("╔═══════════════[Referencing]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject Player;
	public GameObject main_camera;
	[Space (10)]
	public GameObject Rune_1;
	public GameObject Rune_2;
	public GameObject Rune_3;
	public GameObject Rune_4;
	public GameObject Rune_5;
	[Space (10)]
	public GameObject UI_Rune_Manager;
	public GameObject UI_Rune_Manager_Start_Pos;
	[Space (10)]
	public GameObject UI_Rune_1;
	public GameObject UI_Rune_2;
	public GameObject UI_Rune_3;
	public GameObject UI_Rune_4;
	public GameObject UI_Rune_5;
	[Space (10)]
	// public Transform UI_Rune_Radial_1;
	// [Space(10)]
	public Text UI_Text_Rune_1;
	public Text UI_Text_Rune_2;
	public Text UI_Text_Rune_3;
	public Text UI_Text_Rune_4;
	public Text UI_Text_Rune_5;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[UI Rune]════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float UI_Rune_Manager_Offset;
	public float UI_Rune_Manager_Timer_x;

	private Vector2 velocity;
	public float UI_Rune_Manager_Position_x;
	private float UI_Rune_Manager_Position_y;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Cursor]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public string Cursor_Current;
	[Space (10)]
	//public Texture2D cursorTarget;
	public Texture2D Cursor_FFF;
	public Texture2D Cursor_TFT;
	public Texture2D Cursor_TFF;
	public Texture2D Cursor_TTF;
	public Texture2D Cursor_TTT;
	public Texture2D Cursor_FTT;
	public Texture2D Cursor_FFT;
	public Texture2D Cursor_FTF;
	[Space (10)]
	private Vector2 hotSpot = Vector2.zero;
	private CursorMode cursorMode = CursorMode.ForceSoftware;
	public bool Mouse_Right_Down;
	public bool Mouse_Left_Down;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Rune 1]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float Rune_1_Timeout; //In Seconds
	public float Rune_1_CurrentTime = 0;
	public string Rune_1_State = "Idle";
	public float Rune_1_UpForce;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Rune 2]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public float Rune_2_Timeout; //In Seconds
	public float Rune_2_CurrentTime = 0;
	public string Rune_2_State = "Idle";
	public float Rune_2_Damage;
	public GameObject Rune_2_Projectile;
	public float Rune_2_Ghost_Creation_Timer;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Rune 3]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	[Range (0f, 2f)]
	public float Rune_3_Timeout; //In Seconds

	public float Rune_3_CurrentTime = 0;
	public string Rune_3_State = "Idle";

	[Range (0f, 1.5f)]
	public float Rune_3_DodgeTime;

	[Range (0.1f, 50f)]
	public float Rune_3_DodgeSpeed;

	[Range (0f, 2f)]
	public float Rune_3_Invincible;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Rune 4]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject Rune_4_Explosion;
	public GameObject Rune_4_ExplosionCollision;
	[Space (10)]
	public float Rune_4_Timeout; //In Seconds
	public float Rune_4_CurrentTime = 0;
	public string Rune_4_State = "Idle";
	[Space (10)]
	public float Rune_4_Damage;
	[Space (10)]
	[Range (0f, 2f)]
	public float Rune_4_Invincible;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Rune 5]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject Rune_5_Projectile;
	[Space (10)]
	public GameObject Rune_5_Magic;
	public GameObject[] Rune_5_Particles;
	public GameObject Rune_5_Particles_SpawnPosition;
	public GameObject Rune_5_Particles_EndPosition;

	public float Rune_5_Particles_Next;

	public float Rune_5_Timeout; //In Seconds
	public float Rune_5_CurrentTime = 0;
	public string Rune_5_State = "Idle";
	[Space (10)]
	public float Rune_5_Damage;
	[Range (0f, 20f)]
	public float Rune_5_Invincible;

	void Awake () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		hotSpot = new Vector2 (Cursor_FFF.width / 2, Cursor_FFF.height / 2); //Setting cursor hotspot to centre of image
		Cursor.SetCursor (Cursor_FFF, hotSpot, cursorMode);
	}

	void Update () {
		if (!PauseMenu.GameIsPaused) {
			if (GameManager.Instance.PlayerHealth > 0) {
				RuneIsCollected ();
			} else if (Cursor_Current != "FFF") {
				Cursor.SetCursor (Cursor_FFF, hotSpot, cursorMode);
			}

			Mouse_Down ();
			Mouse_Cursor ();
			RuneTimeout_UI ();

		} else { //Prevents Mouse from being target in pause menu
			Mouse_Right_Down = false;
			Cursor.SetCursor (Cursor_FFF, hotSpot, cursorMode);
		}
	}

	void LateUpdate () { //Before any frame is rendered, if Rune_x_CurrentTime < 0 then Rune_x_CurrentTim = 0
		if (Rune_1_CurrentTime < 0) {
			Rune_1_CurrentTime = 0;
		}
		if (Rune_2_CurrentTime < 0) {
			Rune_2_CurrentTime = 0;
		}
		if (Rune_3_CurrentTime < 0) {
			Rune_3_CurrentTime = 0;
		}
		if (Rune_4_CurrentTime < 0) {
			Rune_4_CurrentTime = 0;
		}
		if (Rune_5_CurrentTime < 0) {
			Rune_5_CurrentTime = 0;
		}
	}

	// Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
	// This is creating a CoRoutine which runs independently of the function it is called from
	// StartCoroutine (Countdown (3f, () => {CODE_HERE}));
	IEnumerator Countdown (float seconds, Action onComplete) {
		if (!PauseMenu.GameIsPaused) {
			yield return new WaitForSecondsRealtime (seconds);
			onComplete ();
		}
	}

	void Mouse_Down () {
		if (Input.GetMouseButtonDown (0)) {
			Mouse_Left_Down = true;
		} else if (Input.GetMouseButtonUp (0)) {
			Mouse_Left_Down = false;
		}
		if (Input.GetMouseButtonDown (1)) {
			Mouse_Right_Down = true;
		} else if (Input.GetMouseButtonUp (1)) {
			Mouse_Right_Down = false;
		}
	}

	void Mouse_Cursor () { //Different cursor based on powers that are/arent ready
		if ((Cursor_Current != "FFF") && (Rune_2_State != "Ready") && (Rune_4_State != "Ready") && (Rune_5_State != "Ready")) {
			Cursor.SetCursor (Cursor_FFF, hotSpot, cursorMode);
			Cursor_Current = "FFF";
		} else if ((Cursor_Current != "TFT") && (Rune_2_State == "Ready") && (Rune_4_State != "Ready") && (Rune_5_State == "Ready")) {
			Cursor.SetCursor (Cursor_TFT, hotSpot, cursorMode);
			Cursor_Current = "TFT";
		} else if ((Cursor_Current != "TFF") && (Rune_2_State == "Ready") && (Rune_4_State != "Ready") && (Rune_5_State != "Ready")) {
			Cursor.SetCursor (Cursor_TFF, hotSpot, cursorMode);
			Cursor_Current = "TFF";
		} else if ((Cursor_Current != "TTF") && (Rune_2_State == "Ready") && (Rune_4_State == "Ready") && (Rune_5_State != "Ready")) {
			Cursor.SetCursor (Cursor_TTF, hotSpot, cursorMode);
			Cursor_Current = "TTF";
		} else if ((Cursor_Current != "TTT") && (Rune_2_State == "Ready") && (Rune_4_State == "Ready") && (Rune_5_State == "Ready")) {
			Cursor.SetCursor (Cursor_TTT, hotSpot, cursorMode);
			Cursor_Current = "TTT";
		} else if ((Cursor_Current != "FTT") && (Rune_2_State != "Ready") && (Rune_4_State == "Ready") && (Rune_5_State == "Ready")) {
			Cursor.SetCursor (Cursor_FTT, hotSpot, cursorMode);
			Cursor_Current = "FTT";
		} else if ((Cursor_Current != "FFT") && (Rune_2_State != "Ready") && (Rune_4_State != "Ready") && (Rune_5_State == "Ready")) {
			Cursor.SetCursor (Cursor_FFT, hotSpot, cursorMode);
			Cursor_Current = "FFT";
		} else if ((Cursor_Current != "FTF") && (Rune_2_State != "Ready") && (Rune_4_State == "Ready") && (Rune_5_State != "Ready")) {
			Cursor.SetCursor (Cursor_FTF, hotSpot, cursorMode);
			Cursor_Current = "FTF";
		}

	}

	void RuneIsCollected () {
		if (GameManager.Instance.UnlockedRune[1]) { //If the rune has been collected (Reference to the RuneFollowing script inside this rune)
			UI_Rune_1.SetActive (true); //Activates the gameobject
			Power_1 (); //Calls the respective rune Power
		} else {
			UI_Rune_1.SetActive (false); //Deactivates the gameobject
		}

		if (GameManager.Instance.UnlockedRune[2]) {
			UI_Rune_2.SetActive (true);
			Power_2 ();
		} else {
			UI_Rune_2.SetActive (false);
		}

		if (GameManager.Instance.UnlockedRune[3]) {
			UI_Rune_3.SetActive (true);
			if (!GetComponent<PlayerMovement> ().PlayerPaused) {
				Power_3 ();
			}
		} else {
			UI_Rune_3.SetActive (false);
		}

		if (GameManager.Instance.UnlockedRune[4]) {
			UI_Rune_4.SetActive (true);
			Power_4 ();
		} else {
			UI_Rune_4.SetActive (false);
		}

		if (GameManager.Instance.UnlockedRune[5]) {
			UI_Rune_5.SetActive (true);
			Power_5 ();
		} else {
			UI_Rune_5.SetActive (false);
		}
	}

	void RuneTimeout_UI () { //Visual interface where the player can see which runes have been collected + see their respective timeouts
		if (GameManager.Instance.UIRuneEnabled == "OFF") {
			UI_Rune_Manager.SetActive (false);
		} else {
			UI_Rune_Manager.SetActive (true);
		}

		if (UI_Rune_5.activeSelf) { //Checks if UI_Rune is active
			UI_Rune_Manager_Offset = -0.6f; //Sets the x offset for the UI menu with visual timeouts of the runes
		} else if (UI_Rune_4.activeSelf) {
			UI_Rune_Manager_Offset = -1.55f;
		} else if (UI_Rune_3.activeSelf) {
			UI_Rune_Manager_Offset = -2.5f;
		} else if (UI_Rune_2.activeSelf) {
			UI_Rune_Manager_Offset = -3.45f;
		} else if (UI_Rune_1.activeSelf) {
			UI_Rune_Manager_Offset = -4.4f;
		} else {
			UI_Rune_Manager_Offset = -6f;
		}
		//Math for the smoothing movement until it hits the desired target
		UI_Rune_Manager_Position_y = Mathf.SmoothDamp (UI_Rune_Manager.transform.position.y, UI_Rune_Manager.transform.position.y, ref velocity.y, 0);
		UI_Rune_Manager_Position_x = Mathf.SmoothDamp (UI_Rune_Manager.transform.position.x, (UI_Rune_Manager_Start_Pos.transform.position.x + UI_Rune_Manager_Offset), ref velocity.x, UI_Rune_Manager_Timer_x);
		UI_Rune_Manager.transform.position = new Vector3 (UI_Rune_Manager_Position_x, UI_Rune_Manager_Position_y, 0);
	}

	void Power_1 () {
		if (GameManager.Instance.UIRuneEnabled == "RADIAL") {
			UI_Rune_1.transform.GetComponent<Image> ().fillAmount = 1 - (Rune_1_CurrentTime / Rune_1_Timeout);
			UI_Text_Rune_1.text = "";
		} else if (GameManager.Instance.UIRuneEnabled == "NUMERICAL") {
			UI_Text_Rune_1.text = (Math.Floor (Rune_1_CurrentTime * 10f) / 10f).ToString ();
			UI_Rune_1.transform.GetComponent<Image> ().fillAmount = 1;
		}

		if (Rune_1_CurrentTime > 0) { //Countdown from Timeout to 0 using real time (regardless of framerate)
			//if (!GetComponent<PlayerMovement> ().PlayerPaused) {
			Rune_1_CurrentTime -= (1 * Time.deltaTime);
			//}
		} else if (Rune_1_CurrentTime <= 0) { //When CurrentTime reaches 0 then the bubble can be activated again 
			Rune_1_CurrentTime = 0;
			Rune_1_State = "Ready";
			animator.SetBool ("bubble", false);
			main_camera.GetComponent<PlayerManager> ().invincible = false;
		}
		if (Input.GetKeyUp (KeyCode.Space)) { //When space is released and player on the air then say he has jumped before
			if (!Player.GetComponent<PlayerMovement> ().isGrounded) {
				Player.GetComponent<PlayerMovement> ().hasJumped = true;
			}
			Rune_1_State = "Idle"; //When space is released the Bubble will be disabled
			animator.SetBool ("bubble", false);
			main_camera.GetComponent<PlayerManager> ().invincible = false;
		}
		if (Input.GetKey (KeyCode.Space)) { //When Space is pressed and the cooldown is reset and player on the air activate bubble and set timeout
			if ((Rune_1_State == "Ready") && (!Player.GetComponent<PlayerMovement> ().isGrounded) && (Player.GetComponent<PlayerMovement> ().hasJumped)) {
				Rune_1_CurrentTime = Rune_1_Timeout;
				Rune_1_State = "Active";
				//main_camera.GetComponent<PlayerManager> ().invincible = true;
			}
		}
		if (Rune_1_State == "Active") { //Bubble going up
			if (!GetComponent<PlayerMovement> ().PlayerPaused) {
				rb.velocity = Vector2.up * Rune_1_UpForce;
			}
			Player.GetComponent<PlayerMovement> ().hasJumped = false;
			animator.SetBool ("bubble", true);
		}
	}

	void Power_2 () {
		if (GameManager.Instance.UIRuneEnabled == "RADIAL") {
			UI_Rune_2.transform.GetComponent<Image> ().fillAmount = 1 - (Rune_2_CurrentTime / Rune_2_Timeout);
			UI_Text_Rune_2.text = "";
		} else if (GameManager.Instance.UIRuneEnabled == "NUMERICAL") {
			UI_Text_Rune_2.text = (Math.Floor (Rune_2_CurrentTime * 10f) / 10f).ToString ();
			UI_Rune_2.transform.GetComponent<Image> ().fillAmount = 1;
		}

		if (Rune_2_CurrentTime > 0) { //Countdown from Timeout to 0 using real time (regardless of framerate)
			Rune_2_CurrentTime -= (1 * Time.deltaTime);
		} else if ((Rune_2_CurrentTime <= 0) && (Rune_2_State != "In_Progress")) {
			Rune_2_CurrentTime = 0;
			Rune_2_State = "Ready";
		}

		if ((Rune_2_State == "Ready") && (Mouse_Left_Down)) {
			Rune_2_State = "In_Progress";
		}

		if (Rune_2_State == "In_Progress") {
			var Projectile_Instance = (GameObject) Instantiate (Rune_2_Projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
			Projectile_Instance.GetComponent<Projectile> ().shooterID = "Player_Rune_2";
			Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = Rune_2_Damage;
			Rune_2_CurrentTime = Rune_2_Timeout;
			Rune_2_State = "Idle";
		}

		// if (Rune_2_State == "In_Progress") {
		// 	var Projectile_Instance = (GameObject) Instantiate (Rune_2_Projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
		// 	Projectile_Instance.GetComponent<Projectile> ().shooterID = "Player";
		// 	Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = Rune_2_Damage;
		// 	Projectile_Instance.GetComponent<Projectile> ().explode = true;
		// 	Rune_2_CurrentTime = Rune_2_Timeout;
		// 	Rune_2_State = "Attack_Ghosts";
		// }
		// if (Rune_2_State == "Attack_Ghosts") {
		// 	var Projectile_Instance = (GameObject) Instantiate (Rune_2_Projectile, transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
		// 	Projectile_Instance.GetComponent<Projectile> ().shooterID = "Player";
		// 	Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = 0;
		// 	Projectile_Instance.GetComponent<Projectile> ().explode = false;
		// 	StartCoroutine (Countdown (Rune_2_Ghost_Creation_Timer, () => { Rune_2_State = "Idle"; }));
		// }
	}

	void Power_3 () {
		if (GameManager.Instance.UIRuneEnabled == "RADIAL") {
			UI_Rune_3.transform.GetComponent<Image> ().fillAmount = 1 - (Rune_3_CurrentTime / Rune_3_Timeout);
			UI_Text_Rune_3.text = "";
		} else if (GameManager.Instance.UIRuneEnabled == "NUMERICAL") {
			UI_Text_Rune_3.text = (Math.Floor (Rune_3_CurrentTime * 10f) / 10f).ToString ();
			UI_Rune_3.transform.GetComponent<Image> ().fillAmount = 1;
		}

		if (Rune_3_CurrentTime > 0) { //Countdown from Timeout to 0 using real time (regardless of framerate)
			Rune_3_CurrentTime -= (1 * Time.deltaTime);
		} else if (Rune_3_CurrentTime <= 0) {
			Rune_3_CurrentTime = 0;
			Rune_3_State = "Ready";
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) { //When L_Shift is pressed
			if (Rune_3_State == "Ready") {
				Rune_3_CurrentTime = Rune_3_Timeout;
				Rune_3_State = "In_Progress";
				StartCoroutine (Countdown (Rune_3_Invincible, () => { main_camera.GetComponent<PlayerManager> ().invincible = false; }));
			}
		}

		if (Rune_3_State == "In_Progress") {
			//main_camera.GetComponent<PlayerManager> ().invincible = true;

			if (Player.GetComponent<PlayerMovement> ().facingRight) { //To know where to dogde to
				//rb.AddForce (Vector3.forward * Rune_3_DodgeSpeed);
				Player.transform.position += new Vector3 (Rune_3_DodgeSpeed * Time.deltaTime, 0, 0);
				StartCoroutine (Countdown (Rune_3_DodgeTime, () => { Rune_3_State = "Idle"; }));
			} else {
				Player.transform.position += new Vector3 (-Rune_3_DodgeSpeed * Time.deltaTime, 0, 0);
				StartCoroutine (Countdown (Rune_3_DodgeTime, () => { Rune_3_State = "Idle"; }));
			}
		}
	}

	void Power_4 () {
		if (GameManager.Instance.UIRuneEnabled == "RADIAL") {
			UI_Rune_4.transform.GetComponent<Image> ().fillAmount = 1 - (Rune_4_CurrentTime / Rune_4_Timeout);
			UI_Text_Rune_4.text = "";
		} else if (GameManager.Instance.UIRuneEnabled == "NUMERICAL") {
			UI_Text_Rune_4.text = (Math.Floor (Rune_4_CurrentTime * 10f) / 10f).ToString ();
			UI_Rune_4.transform.GetComponent<Image> ().fillAmount = 1;
		}

		if (Rune_4_CurrentTime > 0) { //Countdown from Timeout to 0 using real time (regardless of framerate)
			Rune_4_CurrentTime -= (1 * Time.deltaTime);
		} else if (Rune_4_CurrentTime <= 0) {
			Rune_4_CurrentTime = 0;
			Rune_4_State = "Ready";
		}

		if ((Rune_4_State == "Ready") && (Input.GetKey (KeyCode.E))) {
			Rune_4_CurrentTime = Rune_4_Timeout;
			Rune_4_State = "In_Progress";
			Rune_4_Explosion.SetActive (false);
			StartCoroutine (Countdown (Rune_4_Invincible + 0.5f, () => { main_camera.GetComponent<PlayerManager> ().invincible = false; }));
		}

		if (Rune_4_State == "In_Progress") {
			main_camera.GetComponent<PlayerManager> ().invincible = true;
			Rune_4_State = "Check_Ground";
		}

		if (Rune_4_State == "Check_Ground") {
			if (GetComponent<PlayerMovement> ().isGrounded) {
				Rune_4_State = "Jump";
				StartCoroutine (Countdown (1.05f, () => { GetComponent<PlayerMovement> ().PlayerPaused = false; Rune_4_ExplosionCollision.SetActive (false); }));
			} else {
				Rune_4_State = "Explode";
				StartCoroutine (Countdown (0.75f, () => { GetComponent<PlayerMovement> ().PlayerPaused = false; Rune_4_ExplosionCollision.SetActive (false); }));
			}

		}

		if (Rune_4_State == "Jump") {
			GetComponent<PlayerMovement> ().Jump ();
			StartCoroutine (Countdown (0.30f, () => { Rune_4_State = "Explode"; }));
		}

		if (Rune_4_State == "Explode") {
			GetComponent<PlayerMovement> ().PlayerPaused = true;
			Rune_4_Explosion.SetActive (true);
			Rune_4_ExplosionCollision.SetActive (true);
			Rune_4_State = "Idle";
		}
	}

	void Power_5 () {
		if (GameManager.Instance.UIRuneEnabled == "RADIAL") {
			UI_Rune_5.transform.GetComponent<Image> ().fillAmount = 1 - (Rune_5_CurrentTime / Rune_5_Timeout);
			UI_Text_Rune_5.text = "";
		} else if (GameManager.Instance.UIRuneEnabled == "NUMERICAL") {
			UI_Text_Rune_5.text = (Math.Floor (Rune_5_CurrentTime * 10f) / 10f).ToString ();
			UI_Rune_5.transform.GetComponent<Image> ().fillAmount = 1;
		}

		if (Rune_5_CurrentTime > 0) { //Countdown from Timeout to 0 using real time (regardless of framerate)
			Rune_5_CurrentTime -= (1 * Time.deltaTime);
		} else if (Rune_5_CurrentTime <= 0) {
			Rune_5_CurrentTime = 0;
			Rune_5_State = "Ready";
		}

		if ((Rune_5_State == "Ready") && (Mouse_Right_Down)) {
			Rune_5_CurrentTime = Rune_5_Timeout;
			Rune_5_State = "In_Progress";
			StartCoroutine (Countdown (Rune_5_Invincible + 0.5f, () => { main_camera.GetComponent<PlayerManager> ().invincible = false; Rune_5_Magic.SetActive (false); }));
		}

		if (Rune_5_State == "In_Progress") {
			main_camera.GetComponent<PlayerManager> ().invincible = true;
			Rune_5_State = "Enabling";
			StartCoroutine (Countdown (3.5f, () => { GetComponent<PlayerMovement> ().PlayerPaused = false; }));
		}

		if (Rune_5_State == "Enabling") {
			GetComponent<PlayerMovement> ().PlayerPaused = true;
			for (int i = 0; i < Rune_5_Particles.Length; i++) {
				//Rune_5_Particles[i].SetActive (false);
				Rune_5_Particles[i].transform.position = Rune_5_Particles_SpawnPosition.transform.position;
				Rune_5_Particles[i].GetComponent<ParticleFollowing> ().desiredParticlePos = Rune_5_Particles_EndPosition;
				Rune_5_Particles[i].GetComponent<ParticleFollowing> ().Rune_5_State = "Charging_1";
			}
			Rune_5_Magic.SetActive (true);
			Rune_5_State = "Charging_1";
		}

		if (Rune_5_State == "Charging_1") {
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 0, () => { Rune_5_Particles[0].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 1, () => { Rune_5_Particles[1].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 2, () => { Rune_5_Particles[2].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 3, () => { Rune_5_Particles[3].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 4, () => { Rune_5_Particles[4].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 5, () => { Rune_5_Particles[5].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 6, () => { Rune_5_Particles[6].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next + Rune_5_Particles_Next * 7, () => { Rune_5_Particles[7].SetActive (true); }));
			StartCoroutine (Countdown (Rune_5_Particles_Next * 8, () => { Rune_5_State = "Charging_2"; }));
			Rune_5_State = "";
		}

		if (Rune_5_State == "Charging_2") {
			for (int i = 0; i < Rune_5_Particles.Length; i++) {
				Rune_5_Particles[i].GetComponent<ParticleFollowing> ().Rune_5_State = "Charging_2";
			}
			Rune_5_State = "";
			StartCoroutine (Countdown (Rune_5_Particles_Next, () => { Rune_5_State = "Merging"; }));
		}

		if (Rune_5_State == "Merging") {
			for (int i = 0; i < Rune_5_Particles.Length; i++) {
				Rune_5_Particles[i].GetComponent<ParticleFollowing> ().Rune_5_State = "Merging";
			}
			Rune_5_State = "";
			StartCoroutine (Countdown (Rune_5_Particles_Next * 4, () => { Rune_5_State = "Charged"; }));
		}

		if (Rune_5_State == "Charged") {
			for (int i = 0; i < Rune_5_Particles.Length; i++) {
				Rune_5_Particles[i].SetActive (false);
			}
			var Projectile_Instance = (GameObject) Instantiate (Rune_5_Projectile, Rune_5_Particles_EndPosition.transform.position, Quaternion.identity); //Shoot a projectile from the enemies body with no rotation
			Projectile_Instance.GetComponent<Projectile> ().shooterID = "Player_Rune_5";
			Projectile_Instance.GetComponent<Projectile> ().ProjectileDamage = Rune_5_Damage;
			Rune_5_State = "";
		}

		if ((Rune_5_State == "Idle") || (Rune_5_State == "Ready")) { }
	}
}