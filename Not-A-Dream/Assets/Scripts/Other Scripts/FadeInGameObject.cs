using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInGameObject : MonoBehaviour {

	SpriteRenderer rend;

	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		Color c = rend.material.color;
		c.a = 0f;
		rend.material.color = c;
	}

	IEnumerator FadeIn () {
		for (float f = 0.05f; f <= 1; f += 0.05f) {
			Color c = rend.material.color;
			c.a = f;
			rend.material.color = c;
			yield return new WaitForSecondsRealtime (0.05f);
		}
	}

	public void StartFading () {
		StartCoroutine ("FadeIn");
	}
}