using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFreeze : MonoBehaviour {

	///public int spdFreeze = 3;
	private GameObject player02;
	private PaddleController freeze;

	void Start(){
		player02 = GameObject.Find ("Player02");
		freeze = player02.GetComponent<PaddleController> ();
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Ball") {
			Debug.Log ("Freeze Red");
			freeze.speed = 3;
			Invoke ("unFreeze", 2);
			Destroy (gameObject);
		}
	}

	void unFreeze(){
		freeze.speed = 7;
	}
}
