using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour {

	public float shrink = 0.75f;

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Ball") {
			Debug.Log ("Shrink");
			other.transform.localScale *= shrink;
			Destroy (gameObject);
		}
	}
}
