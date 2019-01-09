using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] public string UIRuneEnabled;

	[SerializeField] public string HelpArrowsEnabled;

	[SerializeField] public bool HealthBarEnabled;

	protected virtual void Start () {
		SceneManager.LoadScene ("StartMenu");

		UIRuneEnabled = "RADIAL";

		HelpArrowsEnabled = "AUTO";

		HealthBarEnabled = true;
	}

	protected virtual void Update () {
		//Debug.Log(UIRuneEnabled);
	}
}