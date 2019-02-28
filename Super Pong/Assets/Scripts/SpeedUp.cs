using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {

	public int spdAmount = 20;

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Ball") {
			Debug.Log ("Speed Up");
			BallController spdUp = other.gameObject.GetComponent<BallController> ();
			spdUp.SpeedUp (spdAmount);
			Destroy (gameObject);
		}
	}
}
