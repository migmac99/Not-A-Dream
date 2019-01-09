using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public float backgroundSize; //Size of image X in editor
	public float viewZone = 10;
	public float parallaxSpeedX;
	public float parallaxSpeedY;

	private Transform cameraTransform;
	private Transform[] Layers;
	private int leftImage;
	private int rightImage;
	private float lastCameraX;
	private float lastCameraY;

	private void Start () {
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		lastCameraY = cameraTransform.position.y;
		Layers = new Transform[transform.childCount]; //Creating a transform array of size 3 because the GameObject has 3 children
		for (int i = 0; i < transform.childCount; i++) {
			Layers[i] = transform.GetChild (i);
		}
		leftImage = 0;
		rightImage = Layers.Length - 1;
	}

	private void Update () {
		float deltaX = cameraTransform.position.x - lastCameraX;
		transform.position += Vector3.right * (deltaX * parallaxSpeedX);
		lastCameraX = cameraTransform.position.x;

		float deltaY = cameraTransform.position.y - lastCameraY;
		transform.position += Vector3.up * (deltaY * parallaxSpeedY);
		lastCameraY = cameraTransform.position.y;

		if (cameraTransform.position.x < (Layers[leftImage].transform.position.x + viewZone)) {
			ScrollLeft ();
		}
		if (cameraTransform.position.x > (Layers[rightImage].transform.position.x - viewZone)) {
			ScrollRight ();
		}
	}

	private void ScrollLeft () {
		int lastRight = rightImage;
		Layers[rightImage].position = new Vector3 (Layers[leftImage].position.x - backgroundSize, Layers[leftImage].position.y, 0); //Teleporting the rightmost image to the leftmost image position
		leftImage = rightImage;
		rightImage--;
		if (rightImage < 0) { //Eliminates out of range exception
			rightImage = Layers.Length - 1;
		}
	}

	private void ScrollRight () {
		int lastLeft = leftImage;
		Layers[leftImage].position = new Vector3 (Layers[rightImage].position.x + backgroundSize, Layers[rightImage].position.y, 0); //Teleporting the leftmost image to the rightmost image position
		rightImage = leftImage;
		leftImage++;
		if (leftImage == Layers.Length) { //Eliminates out of range exception
			leftImage = 0;
		}
	}
}