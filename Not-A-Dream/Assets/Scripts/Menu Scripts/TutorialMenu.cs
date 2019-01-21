using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour {

	[Header ("╔═══════════════[References to Menus]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject backMenuUI;
	[Space (10)]
	public GameObject tutorialMenuUI;
	[Space (10)]
	public GameObject[] UnlockedRuneStrike;

	void Start () {
		for (int i = 1; i < 6; i++) {
			if (GameManager.Instance.UnlockedRune[i]) {
				//Debug.Log ("DEACTIVATED STRIKE [" + i + "]");
				UnlockedRuneStrike[i].SetActive (false);
			} else {
				UnlockedRuneStrike[i].SetActive (true);
			}
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (tutorialMenuUI.activeSelf) {
				Back ();
			}
		}
	}

	public void StartBasicTutorial () {
		SceneManager.LoadScene ("Basic_Tutorial");
		GameManager.Instance.UnlockedContinue = true;
	}

	public void StartRune_1_Tutorial () {
		if (GameManager.Instance.UnlockedRune[1]) {
			SceneManager.LoadScene ("Rune_1_Tutorial");
		}
	}

	public void StartRune_2_Tutorial () {
		if (GameManager.Instance.UnlockedRune[2]) {
			SceneManager.LoadScene ("Rune_2_Tutorial");
		}
	}

	public void StartRune_3_Tutorial () {
		if (GameManager.Instance.UnlockedRune[3]) {
			SceneManager.LoadScene ("Rune_3_Tutorial");
		}
	}

	public void StartRune_4_Tutorial () {
		if (GameManager.Instance.UnlockedRune[4]) {
			SceneManager.LoadScene ("Rune_4_Tutorial");
		}
	}

	public void StartRune_5_Tutorial () {
		if (GameManager.Instance.UnlockedRune[5]) {
			SceneManager.LoadScene ("Rune_5_Tutorial");
		}
	}

	public void Back () {
		tutorialMenuUI.SetActive (false);
		backMenuUI.SetActive (true);
	}
}