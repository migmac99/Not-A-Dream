using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour {

	public GameObject[] Elevator;

	void Update () {
		for (int i = 1; i < Elevator.Length; i++) {
			if (GameManager.Instance.UnlockedElevator[i]) {
				Elevator[i].SetActive (true);
			}
		}
	}
}