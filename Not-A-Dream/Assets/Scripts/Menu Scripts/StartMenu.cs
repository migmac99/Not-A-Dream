using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	[Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject selectedMenuUI;
	[Space (10)]
	public GameObject settingsMenuUI;

	void Update () {

	}

	public void StartGame () {
		//Debug.Log ("Loading Game...");
		SceneManager.LoadScene ("not-a-dream");
	}

	public void LoadSettings () {
		//Debug.Log ("Loading Settings...");
		selectedMenuUI.SetActive (false);
		settingsMenuUI.SetActive (true);
	}

	public void QuitGame () {
		Debug.Log ("Quitting Game...");
		Application.Quit ();
	}
}