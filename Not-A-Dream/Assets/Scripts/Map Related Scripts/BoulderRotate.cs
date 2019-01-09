using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRotate1 : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
		//this.transform.Rotate (Vector3.forward * -90);
	}
}
