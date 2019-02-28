using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UINetwork : MonoBehaviour
{
    
    GameObject panelConnection;
    Button btnHost;
    Button btnJoin;
    Button btnCancel;
    Text txInfo;

    int status = 0;
    
    NetworkManager network;

    // Start is called before the first frame update
    void Start()
    {

        panelConnection = GameObject.Find ("Panel Connection");
        panelConnection.transform.localPosition = Vector3.zero;

        btnHost = GameObject.Find("BtnHost").GetComponent<Button>();
        btnJoin = GameObject.Find("BtnJoin").GetComponent<Button>();
        btnCancel = GameObject.Find("BtnCancel").GetComponent<Button>();
        txInfo = GameObject.Find("Info").GetComponent<Text>();
        btnHost.onClick.AddListener(StartHostGame);
        btnJoin.onClick.AddListener(StartJoinGame);
        btnCancel.onClick.AddListener(CancelConnection);
        btnCancel.interactable = false;

        network = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        txInfo.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
        Debug.Log(network.networkAddress);
    }

    private void CancelConnection()
    {
        network.StopHost();
        btnHost.interactable = true;
        btnJoin.interactable = true;
        btnCancel.interactable = false;
        txInfo.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
    }

    private void StartJoinGame()
    {
        if (!NetworkClient.active)
        {
            network.StartClient();
            network.client.RegisterHandler(MsgType.Disconnect, ConnectionError);
        }
        if (NetworkClient.active) txInfo.text = "Info: Trying connect to server";
    }

    private void ConnectionError(NetworkMessage netMsg)
    {
        //network.StopClient();
        //txInfo.text = "Info: Disconnect from server";
        BacktoMainMenu();
    }

    private void StartHostGame()
    {
        if (!NetworkServer.active)
        {
            network.StartHost();
        }
        if (NetworkServer.active) txInfo.text = "Info: Matching with other player";
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkClient.active || NetworkServer.active)
        {
            btnHost.interactable = false;
            btnJoin.interactable = false;
            btnCancel.interactable = true;
        }
        else
        {
            btnHost.interactable = true;
            btnJoin.interactable = true;
            btnCancel.interactable = false;
        }

        if(NetworkServer.connections.Count == 2 && status == 0)
        {
            status = 1;
            PlayGame();
        }
        if(ClientScene.ready && !NetworkServer.active && status == 0)
        {
            status = 1;
            PlayGame();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            BacktoMenu();
        }
    }

    public void PlayGame()
    {
        panelConnection.transform.localPosition = new Vector3(-1500, 0, 0);
    }

    public void BacktoMainMenu()
    {
        network.StopHost();
        SceneManager.LoadScene("Main");
    }

    public void BacktoMenu()
    {
        network.StopHost();
        SceneManager.LoadScene("Menu");
    }
}
