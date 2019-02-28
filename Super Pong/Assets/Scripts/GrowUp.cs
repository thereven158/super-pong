using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUp : MonoBehaviour {
	public float multiplier = 1.5f;

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Ball") {
			Debug.Log ("Grow Up");
			other.transform.localScale *= multiplier;
			Destroy (gameObject);
		}
	}
}
