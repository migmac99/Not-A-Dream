using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour {

	public GameObject respawnMenuUI;

	public void Awake () {
		respawnMenuUI.SetActive (false);
	}

	public void LoadRespawnMenu () {
		respawnMenuUI.SetActive (true);
	}

	public void Respawn () {
		Time.timeScale = 1f;
		SceneManager.LoadScene (GameManager.Instance.CurrentScene);
		respawnMenuUI.SetActive (false);
		GameManager.Instance.PlayerHealth = 100;
	}

	public void LoadMenu () {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("StartMenu");
		GameManager.Instance.PlayerHealth = 100;
	}

}