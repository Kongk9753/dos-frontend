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

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Panel");
        player1 = GameObject.Find("PlayerName (1)").GetComponent<Text>();;
        player2 = GameObject.Find("PlayerName (2)").GetComponent<Text>();;
        // Subscribe to WebSocketManager's OnMessage event
        WebSocketManager.Instance.OnMessageReceived += HandleMessage;
    }

    void HandleMessage(string message)
    {
        Debug.Log("Received message: " + message);

        // Find the Panel GameObject
        if (panel != null )
        {
            Debug.Log("panel found :)");
            player1.text = "player1";
            player2.text = "player2";

        }
        else
        {
            Debug.LogError("Panel GameObject not found!");
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
