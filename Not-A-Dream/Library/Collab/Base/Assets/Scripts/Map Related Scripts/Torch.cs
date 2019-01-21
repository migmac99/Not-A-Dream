using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	
	void Awake () {
		
	}
	

	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag ("Projectile"))
		{
			for (int a = 0; a < transform.childCount; a++) {
			transform.GetChild (a).gameObject.SetActive(true);
			}
	//	gameObject.SetActive(true);
		}
	}
}
