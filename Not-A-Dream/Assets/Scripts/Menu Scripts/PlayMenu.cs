using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour {

	[Header ("╔═══════════════[References to Menus]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject backMenuUI;
	[Space (10)]
	public GameObject PlayMenuUI;
	[Space (10)]
	public GameObject UnlockedContinueStrike;

	void Start () {
		if (GameManager.Instance.UnlockedContinue) {
			UnlockedContinueStrike.SetActive (false);
		} else {
			UnlockedContinueStrike.SetActive (true);
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (PlayMenuUI.activeSelf) {
				Back ();
			}
		}
	}

	public void NewGame () {
		for (int i = 1; i < 6; i++) {
			GameManager.Instance.UnlockedRune[i] = false;
		}
		GameManager.Instance.CurrentCheckpointPos = new Vector2 (-8, 40);

		//GameManager.Instance.CurrentScene = "Basic_Tutorial";
		GameManager.Instance.CurrentScene = "not-a-dream"; //testing only
		SceneManager.LoadScene (GameManager.Instance.CurrentScene);
		GameManager.Instance.UnlockedContinue = true;
		GameManager.Instance.PlayerHealth = 100;
	}

	public void ContinueGame () {
		if (GameManager.Instance.UnlockedContinue) {
			SceneManager.LoadScene (GameManager.Instance.CurrentScene);
		}
	}

	public void Back () {
		PlayMenuUI.SetActive (false);
		backMenuUI.SetActive (true);
	}
}