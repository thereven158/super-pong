using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBlue : MonoBehaviour {

	//public int spdFreeze = 3;
	private GameObject player01;
	private PaddleController freeze;

	void Start(){
		player01 = GameObject.Find ("Player01");
		freeze = player01.GetComponent<PaddleController> ();
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Ball") {
			Debug.Log ("Freeze Blue");
			freeze.speed = 3;
			Invoke ("unFreeze", 2);
			Destroy (gameObject);
		}
	}

	void unFreeze(){
		freeze.speed = 7;
	}
}
