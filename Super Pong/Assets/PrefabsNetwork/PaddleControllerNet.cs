using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PaddleControllerNet : NetworkBehaviour {
	public float speed;
	public string axis;
	public float boundaryTop;
	public float boundaryBott;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) return;

		float move = Input.GetAxis (axis) * speed * Time.deltaTime;
		float nextPos = transform.position.y + move;

		if (nextPos > boundaryTop) {
			move = 0;
		}
		if (nextPos < boundaryBott) {
			move = 0;
		}
		transform.Translate(0, move, 0);
	}

    private void Awake()
    {
        if (transform.position.x > 0) transform.GetComponent<SpriteRenderer>().color = Color.red;
        else transform.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
