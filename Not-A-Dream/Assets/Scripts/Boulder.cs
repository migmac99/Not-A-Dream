using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

 public float movementSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);

	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject.tag == "Destroy")
		{
			Debug.Log("Hi");
			Destroy(gameObject);
		}
	}
}
