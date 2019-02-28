using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	float randX;
	float randY;
	Vector2 whereToSpawn;
	public float spawnRate = 5f;
	float nextSpawn = 0.0f;
	public GameObject[] PowerUp;
	int randIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + spawnRate;
			randX = Random.Range (-7.0f, 7.0f);
			randY = Random.Range (-3.5f, 3.5f);
			randIndex = Random.Range (0,5);
			whereToSpawn = new Vector2 (randX, randY);
			Instantiate (PowerUp[randIndex], whereToSpawn, Quaternion.identity);
		}
	}
}
