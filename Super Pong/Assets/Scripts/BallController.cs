using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
	public int force;
	Rigidbody2D rigid;
    int scoreP1;
    int scoreP2;
	int randInt;
	Text scoreUIP1;
	Text scoreUIP2;
	Text ballSpdUI;
	GameObject panelEnd;
	Text txtWinner;
	Vector2 direction;
    AudioSource audioSound;
    public AudioClip hitSound;
	private PaddleController unFreeze01, unFreeze02;
	private GameObject player01, player02;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D> ();
        audioSound = GetComponent<AudioSource> ();

		//unFreeze player01
		player01 = GameObject.Find ("Player01");
		unFreeze01 = player01.GetComponent<PaddleController> ();

		//unFreeze player02
		player02 = GameObject.Find ("Player02");
		unFreeze02 = player02.GetComponent<PaddleController> ();

		//Random ball mental (left or right)
		randInt = Random.Range (0, 100);
		Debug.Log ("random Int = " + randInt);
		if (randInt > 50) {
			direction = new Vector2 (2, 0).normalized;
		} else {
			direction = new Vector2 (-2, 0).normalized;
		}

		rigid.AddForce (direction * force);
		scoreP1 = 0;
		scoreP2 = 0;
		scoreUIP1 = GameObject.Find ("Score1").GetComponent<Text> ();
		scoreUIP2 = GameObject.Find ("Score2").GetComponent<Text> ();
		ballSpdUI = GameObject.Find ("BallSpdTxt").GetComponent<Text> ();
		panelEnd = GameObject.Find ("PanelEnd");
		panelEnd.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ResetBall(){
		transform.localPosition = new Vector2 (0, 0);
		rigid.velocity = new Vector2 (0, 0);
		force = 200;
		transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		unFreeze01.speed = 7;
		unFreeze02.speed = 7;
		ShowBallSpd ();
	}

	private void OnCollisionEnter2D (Collision2D coll){
        //audioSound.PlayOneShot(hitSound);
		if (coll.gameObject.name == "RightWall") {
            scoreP1 += 1;
            ShowScore();
            if (scoreP1 == 5) {
                Debug.Log("Dug");
                panelEnd.SetActive (true);
				txtWinner = GameObject.Find ("Winner").GetComponent<Text> ();
				txtWinner.text = "Blue Player Win!";
				Destroy (gameObject);
                Time.timeScale = 0;
                return;
			}
			ResetBall ();
			Vector2 direction = new Vector2 (2, 0).normalized;
			rigid.AddForce (direction * force);
		}
		if (coll.gameObject.name == "LeftWall") {
			scoreP2 += 1;
            ShowScore();
            if (scoreP2 == 5) {
                Debug.Log("Duga");
                panelEnd.SetActive (true);
				txtWinner = GameObject.Find ("Winner").GetComponent<Text> ();
				txtWinner.text = "Red Player Win!";
				Destroy (gameObject);
                Time.timeScale = 0;
				return;
			}
			ResetBall ();
			Vector2 direction = new Vector2 (-2, 0).normalized;
			rigid.AddForce (direction * force);
		}
		if (coll.gameObject.name == "Player01" || coll.gameObject.name == "Player02") {
			float angle = (transform.position.y - coll.transform.position.y) * 5f;
			Vector2 direction = new Vector2 (rigid.velocity.x, angle).normalized;
			rigid.velocity = new Vector2 (0, 0);
			rigid.AddForce (direction * force * 2);
		} 

		//Ball Speed Up every collision with player
		if (coll.gameObject.name == "Player01" || coll.gameObject.name == "Player02") {
			ShowBallSpd ();
			force += 5;
			//unFreeze
			if (coll.gameObject.name == "Player01") {
				unFreeze01.speed = 7;
			}
			if (coll.gameObject.name == "Player02") {
				unFreeze02.speed = 7;
			}
		}
	}

	public void SpeedUp (int speedAmount){
		force += speedAmount;
		ShowBallSpd ();
	}

    void ShowScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

    void ShowBallSpd(){
		Debug.Log ("Ball Speed Up to " + force);
		ballSpdUI.text = "Ball Speed: " + force;
	}
}
