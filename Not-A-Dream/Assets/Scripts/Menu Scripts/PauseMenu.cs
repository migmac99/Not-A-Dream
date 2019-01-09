using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	[HideInInspector]
	public Animator animator;
	bool HasRanOnce = false;

	[Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject selectedMenuUI;
	[Space (10)]
	public GameObject settingsMenuUI;

	void Awake () {
		animator = selectedMenuUI.GetComponent<Animator> ();
		Resume (); //Makes sure the game doesnt start paused
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
		if ((HasRanOnce) && (selectedMenuUI.activeSelf)) {
			animator.SetBool ("HasRanOnce", true);
		}
	}

	public void Resume () {
		selectedMenuUI.SetActive (false);
		settingsMenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		HasRanOnce = false;
	}

	void Pause () {
		selectedMenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void LoadSettings () {
		//Debug.Log ("Loading Settings...");
		selectedMenuUI.SetActive (false);
		settingsMenuUI.SetActive (true);
		HasRanOnce = true;
	}

	public void LoadMenu () {
		//Debug.Log ("Loading Menu...");
		Time.timeScale = 1f;
		SceneManager.LoadScene ("StartMenu");
	}

	public void QuitGame () {
		Debug.Log ("Quitting Game...");
		Application.Quit ();
	}
}