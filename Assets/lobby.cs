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
    private Text title;
    private string splitText;
    private List<string> players = new List<string>();

    // Start is called before the first frame update
    async void Start()
    {
        Text[] playerNames = new Text[10];
        playerNames[0] = GameObject.Find("PlayerName (1)").GetComponent<Text>();
        playerNames[1] = GameObject.Find("PlayerName (2)").GetComponent<Text>();
        title = GameObject.Find("Title").GetComponent<Text>();
        title.text = "Lobby: " + start_page.Instance.CreateLobbyCode;

        // Subscribe to WebSocketManager's OnMessage event
        WebSocketManager.Instance.OnMessageReceived += (string message) =>
        {
            string[] command = message.Split(":");
            Debug.Log("Received message: " + message);
            Debug.Log("Command: " + command[1]);
            Debug.Log("Code: " + start_page.Instance.CreateLobbyCode);
            if (command[0] == "joined")
            {
                Debug.Log("HEJ");
                players.Add(command[1]);
                Debug.Log(players.Count);
                for (int i = 0; i < players.Count; i++)
                {
                    Debug.Log("HEJ3");
                    playerNames[i].text = players[i];
                }
            }
        };
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
