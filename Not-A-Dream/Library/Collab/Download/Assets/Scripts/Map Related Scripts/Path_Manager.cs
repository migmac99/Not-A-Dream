using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Manager : MonoBehaviour {

	public bool Loop;
	public Color rayColor = Color.white;
	public List<Transform> path_objs = new List<Transform> ();
	public Transform[] theArray;

	void OnDrawGizmos () {
		Gizmos.color = rayColor; //Color of the gizmo rays
		theArray = GetComponentsInChildren<Transform> (); //Array with all of the waypoints
		path_objs.Clear (); //So it can be used more than once

		foreach (Transform path_obj in theArray) { //Adds the paths to every waypoint
			if (path_obj != this.transform) {
				path_objs.Add (path_obj);
			}
		}

		for (int i = 0; i < path_objs.Count; i++) { //For each waypoint create a sphere and draw a path to the next waypoint
			Vector3 position = path_objs[i].position;
			if (i > 0) {
				Vector3 previous = path_objs[i - 1].position;
				Gizmos.DrawWireSphere (previous, 0.15f);
				Gizmos.DrawLine (previous, position);
			}
			if (i == path_objs.Count - 1) { //Closes the loop so it creates a path back to the first element of the array
				Gizmos.DrawWireSphere (position, 0.15f);
				if (Loop) {
					Vector3 first = path_objs[0].position;
					Gizmos.DrawLine (position, first);
				}
			}
		}
	}
}