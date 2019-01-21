using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class HealthGUI : EditorWindow {

	[SerializeField] //Saves stored var on disk
	public GameObject main_camera;

	float PlayerHealth;

	[SerializeField]
	public GameObject Enemy1;
	float EnemyHealth_1;

	[HideInInspector]
	Vector2 scrollPosition;

	[HideInInspector]
	int MenuItems;

	[HideInInspector]
	int UsedSlotsInArray = 0;

	[SerializeField]
	public GameObject[] Enemies;
	public float[] CurrentEnemyHealth;

	public void OnEnable () { }

	[MenuItem ("Window/Health GUI")]
	public static void ShowWindow () {
		GetWindow<HealthGUI> ("Health GUI");
	}

	static void Init () {
		EditorWindow window = GetWindow (typeof (HealthGUI), false, "DisplayInfo");
		window.Show ();
	}

	void Awake () {
		Enemies = new GameObject[100];
		CurrentEnemyHealth = new float[100];
	}

	void OnGUI () {
		if (main_camera == null) {
			EditorGUILayout.HelpBox ("Link the respective GameObjects here so the script can work", MessageType.Info);

			main_camera = (GameObject) EditorGUILayout.ObjectField ("Main Camera", main_camera, typeof (GameObject), true);
		} else {
			EditorGUILayout.HelpBox ("Here you can view player Health, Enemy Health and reset it to 100 by clicking the respective buttons", MessageType.Info);
		}

		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width - 3), GUILayout.Height (Screen.height - 68)); { //250

			//Player
			if (main_camera != null) {
				GUILayout.Space (20);

				GUILayout.BeginVertical (); {
					GUILayout.Label ("PLAYER");
					PlayerHealth = GameManager.Instance.PlayerHealth;

					EditorGUI.ProgressBar (EditorGUILayout.GetControlRect (false, 20), PlayerHealth / 100, "Health [" + GameManager.Instance.PlayerHealth.ToString () + "]");
					GUILayout.BeginHorizontal (); {

						if (GUILayout.Button ("Reset Health [100]")) {
							GameManager.Instance.PlayerHealth = 100;
						}
						if (GUILayout.Button ("Damage Player [10]")) {
							main_camera.GetComponent<PlayerManager> ().TakeDamage (10);
						}
						GUILayout.EndHorizontal ();
					}
					GUILayout.EndVertical ();
				}
			}

			GUILayout.Space (10);

			//Debug.Log (MenuItems + " Menu Items    ||    Array Lenght = " + Enemies.Length);

			for (int CurrentMenuItem = 1; CurrentMenuItem <= MenuItems; CurrentMenuItem++) {
				if (MenuItems > 0) {
					//Debug.Log ("Current Menu Item [" + CurrentMenuItem + "]     || Current Enemy Health [" + CurrentEnemyHealth[CurrentMenuItem] + "]");
					if (Enemies[CurrentMenuItem] == null) {
						Enemies[CurrentMenuItem] = (GameObject) EditorGUILayout.ObjectField ("Enemy [" + CurrentMenuItem + "]", Enemies[CurrentMenuItem], typeof (GameObject), true);
					}
					if (Enemies[CurrentMenuItem] != null) {
						GUILayout.Space (20);

						GUILayout.BeginVertical (); {
							GUILayout.Label ("Enemy [" + CurrentMenuItem + "]");
							CurrentEnemyHealth[CurrentMenuItem] = Enemies[CurrentMenuItem].GetComponent<Firstenemy> ().EnemyHealth;

							EditorGUI.ProgressBar (EditorGUILayout.GetControlRect (false, 20), CurrentEnemyHealth[CurrentMenuItem] / Enemies[CurrentMenuItem].GetComponent<Firstenemy> ().StartEnemyHealth, "Health [" + CurrentEnemyHealth[CurrentMenuItem].ToString () + "]");
							GUILayout.BeginHorizontal (); {

								if (GUILayout.Button ("Reset Health [" + Enemies[CurrentMenuItem].GetComponent<Firstenemy> ().StartEnemyHealth + "]")) {
									Enemies[CurrentMenuItem].GetComponent<Firstenemy> ().EnemyHealth = Enemies[CurrentMenuItem].GetComponent<Firstenemy> ().StartEnemyHealth;
								}
								if (GUILayout.Button ("Damage Enemy [10]")) {
									Enemies[CurrentMenuItem].GetComponent<Firstenemy> ().TakeDamage (10);
								}
								GUILayout.EndHorizontal ();
							}
							GUILayout.EndVertical ();
						}
					}
				}

				GUILayout.Space (10);
			}

			for (int CurrentMenuItem = UsedSlotsInArray; CurrentMenuItem >= 1; CurrentMenuItem--) {
				if (UsedSlotsInArray > MenuItems) {
					Enemies[CurrentMenuItem] = null;
					CurrentEnemyHealth[CurrentMenuItem] = 0;
					UsedSlotsInArray -= 1;
					//Debug.Log ("DELETED MENU ITEM'S [" + CurrentMenuItem + "] DATA");
				}
			}

			GUILayout.BeginVertical (); {
				GUILayout.BeginHorizontal (); {
					GUILayout.FlexibleSpace (); {
						if (GUILayout.Button ("+", GUILayout.Width (25), GUILayout.Height (25))) {
							MenuItems += 1;
							UsedSlotsInArray += 1;
						}
						if (GUILayout.Button ("-", GUILayout.Width (25), GUILayout.Height (25))) {
							if (MenuItems > 0) {
								MenuItems -= 1;
							}
						}
						GUILayout.FlexibleSpace ();
					}
					GUILayout.EndHorizontal ();
				}
				GUILayout.EndVertical ();
				GUILayout.Space (10);
			}

			GUILayout.EndScrollView ();
		}

	}

	void Update () {
		Repaint ();
	}
}

// EditorGUILayout.LabelField( "TITLE: ",   "info note in inspector");
// EditorGUILayout.HelpBox("This is a square message info in inspector", MessageType.Info);