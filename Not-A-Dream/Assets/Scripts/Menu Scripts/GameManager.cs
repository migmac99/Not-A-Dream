﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] public string UIRuneEnabled;

	[SerializeField] public string HelpArrowsEnabled;

	[SerializeField] public bool HealthBarEnabled;

	[SerializeField] public bool[] UnlockedRune;

	[SerializeField] public bool UnlockedContinue = false;

	[SerializeField] public string CurrentScene;

	[SerializeField] public float PlayerHealth;

	[SerializeField] public AudioMixer audioMixer;

	[Space (10)]
	//[SerializeField] public GameObject InitialSpawnPosition;
	[SerializeField] public Vector2 CurrentCheckpointPos;

	protected virtual void Start () {
		UnlockedRune = new bool[6]; //this because array starts on [0] and not [1] ([0] is not being used)
		SceneManager.LoadScene ("StartMenu");

		UIRuneEnabled = "RADIAL";

		HelpArrowsEnabled = "AUTO";

		HealthBarEnabled = true;

		for (int i = 1; i < 6; i++) {
			UnlockedRune[i] = false;
		}

		CurrentCheckpointPos = new Vector2 (-8, 40);
		PlayerHealth = 100;
	}

	protected virtual void Update () {

	}
}