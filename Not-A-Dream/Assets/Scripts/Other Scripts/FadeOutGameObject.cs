using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutGameObject : MonoBehaviour {

	SpriteRenderer rend;
	float localValue;

	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}

	IEnumerator FadeOut () {
		for (float f = 1f; f >= 0; f += -localValue) {
			if (f <= localValue) {
				f = 0;
			}
			rend.material.color = new Color (1, 1, 1, f);

			yield return new WaitForSecondsRealtime (0.05f);
		}
	}

	public void StartFading (float value) {
		StartCoroutine ("FadeOut");
		localValue = value;
		//	GetComponent<SpriteRenderer>().color.a = 0.5f;
	}
}