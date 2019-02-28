using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallManager : NetworkBehaviour
{
    public GameObject prefabBall;
    bool appearBall = false;
    GameObject ball;

    // Update is called once per frame
    void Update()
    {
        if (!isServer || appearBall) return;

        if (NetworkServer.connections.Count == 2)
        {
            ball = GameObject.Instantiate(prefabBall);
            NetworkServer.Spawn(ball);
            appearBall = true;
        }
    }
}
