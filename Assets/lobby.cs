using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class lobby : MonoBehaviour
{
    private GameObject panel;
    private Text player1;
    private Text player2;
    private string splitText;

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Panel");
        player1 = GameObject.Find("PlayerName (1)").GetComponent<Text>(); ;
        player2 = GameObject.Find("PlayerName (2)").GetComponent<Text>(); ;
        // Subscribe to WebSocketManager's OnMessage event
        WebSocketManager.Instance.OnMessageReceived += (string message) =>
        {

            string[] command = message.Split(":");
            Debug.Log("Received message: " + message);
            Debug.Log("Command: " + command[0]);
            splitText = command[0];
            Debug.Log("panel found :)");
            player1.text = "player1";
            player2.text = "player2";

        };
        // if (splitText == "joined")
        // {
        //     Debug.Log("panel found :)");
        //     player1.text = "player1";
        //     player2.text = "player2";
        //     splitText = "";
        // }
        //string[] message =   WebSocketManager.Instance.OnMessageReceived.split(":");
    }

    void HandleMessage(string message)
    {
        string[] command = message.Split(":");
        Debug.Log("Received message: " + message);
        Debug.Log("Command: " + command[0]);
        splitText = command[0];
    }
    // Update is called once per frame
    void Update()
    {
    }
}
