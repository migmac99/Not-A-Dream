using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] public Color CheckpointColor;

	[SerializeField] public string UIRuneEnabled;

	[SerializeField] public string HelpArrowsEnabled;

	[SerializeField] public bool HealthBarEnabled;

	[SerializeField] public bool[] UnlockedRune;

	[SerializeField] public bool[] UnlockedElevator;

	[SerializeField] public bool UnlockedContinue = false;

	[SerializeField] public string CurrentScene;

	[SerializeField] public float PlayerHealth;

	[SerializeField][Range (0, 20)] public int PassiveRegenValue;

	[SerializeField][Range (0, 5)] public int RegenEvery_x_Seconds;

	[SerializeField] public float DirectorCutTime;

	[SerializeField] public float DirectorCameraSpeed;

	[SerializeField] public bool FullScreen;

	[SerializeField] public AudioMixer audioMixer;

	[SerializeField] public Texture2D Cursor_FFF;
	private Vector2 hotSpot;

	[Space (10)]
	//[SerializeField] public GameObject InitialSpawnPosition;
	[SerializeField] public Vector2 CurrentCheckpointPos;
	[Space (10)]
	[SerializeField] public Vector2 TutorialCheckpointPos;

	protected virtual void Start () {
		Screen.fullScreen = true;

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

		hotSpot = new Vector2 (Cursor_FFF.width / 2, Cursor_FFF.height / 2); //Setting cursor hotspot to centre of image
		Cursor.SetCursor (Cursor_FFF, hotSpot, CursorMode.ForceSoftware);
	}

	protected virtual void Update () {

	}
}