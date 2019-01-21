using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	[Header ("╔═══════════════[References]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject selectedMenuUI;
	[Space (10)]
	public GameObject tutorialMenuUI;
	[Space (10)]
	public GameObject settingsMenuUI;
	[Space (10)]
	public GameObject playMenuUI;

	void Awake () {
		selectedMenuUI.SetActive (true);
		tutorialMenuUI.SetActive (false);
		settingsMenuUI.SetActive (false);
		playMenuUI.SetActive (false);
	}

	public void LoadPlayMenu () {
		selectedMenuUI.SetActive (false);
		playMenuUI.SetActive (true);
	}

	public void LoadTutorialMenu () {
		selectedMenuUI.SetActive (false);
		tutorialMenuUI.SetActive (true);
	}

	public void LoadSettings () {
		selectedMenuUI.SetActive (false);
		settingsMenuUI.SetActive (true);
	}

	public void QuitGame () {
		Debug.Log ("Quitting Game...");
		Application.Quit ();
	}
}