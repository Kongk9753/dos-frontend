using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePage : MonoBehaviour
{

    private GameObject cube0;
    private GameObject cube1;
    private GameObject cube2;
    private GameObject cube3;
    private Transform card;
    private pullCard pulls;
    public card findCard;
    private GameObject stopButton;



    // Start is called before the first frame update
    void Start()
    {
        while (WebSocketManager.Instance.players.Count < 4)
        {
            WebSocketManager.Instance.players.Add("dummy");
        }
        Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
        pulls = GameObject.Find("Cards").GetComponent<pullCard>();
        card = GameObject.Find("Cards").transform;

        stopButton = GameObject.Find("StopButton");
        stopButton.SetActive(false);

        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 0)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube0;
            dictionary[WebSocketManager.Instance.players[1]] = cube1;
            dictionary[WebSocketManager.Instance.players[2]] = cube2;
            dictionary[WebSocketManager.Instance.players[3]] = cube3;
            pulls.Pull(card, "Cube0");
            pulls.Pull(card, "Cube1");
            pulls.Pull(card, "Cube2");
            pulls.Pull(card, "Cube3");

            Debug.Log("0");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 1)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube3;
            dictionary[WebSocketManager.Instance.players[1]] = cube0;
            dictionary[WebSocketManager.Instance.players[2]] = cube1;
            dictionary[WebSocketManager.Instance.players[3]] = cube2;
            pulls.Pull(card, "Cube3");
            pulls.Pull(card, "Cube0");
            pulls.Pull(card, "Cube1");
            pulls.Pull(card, "Cube2");
            Debug.Log("1");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 2)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube2;
            dictionary[WebSocketManager.Instance.players[1]] = cube3;
            dictionary[WebSocketManager.Instance.players[2]] = cube0;
            dictionary[WebSocketManager.Instance.players[3]] = cube1;
            pulls.Pull(card, "Cube2");
            pulls.Pull(card, "Cube3");
            pulls.Pull(card, "Cube0");
            pulls.Pull(card, "Cube1");
            Debug.Log("2");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 3)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube1;
            dictionary[WebSocketManager.Instance.players[1]] = cube2;
            dictionary[WebSocketManager.Instance.players[2]] = cube3;
            dictionary[WebSocketManager.Instance.players[3]] = cube0;
            pulls.Pull(card, "Cube1");
            pulls.Pull(card, "Cube2");
            pulls.Pull(card, "Cube3");
            pulls.Pull(card, "Cube0");
            Debug.Log("3");
        }

        WebSocketManager.Instance.Send("ready:now");
        WebSocketManager.Instance.OnMessageReceived += (string message) =>
        {
            UnityMainThreadDispatcher.Dispatcher.Enqueue(() => OnMessageReceived(message));
        };

    }


    private void OnMessageReceived(string message)
    {
        try
        {
            string[] command = message.Split(':');
            Debug.Log("Received message: " + message);


            if (command[0] == "played" && command[1] != "")
            {
                Debug.Log(command[2] + "command2");
                findCard = GameObject.Find(command[2]).GetComponent<card>();
                findCard.LayOthersCard();

            }
            else if (command[0] == "turn")
            {
                Debug.Log("myturn");
                stopButton.SetActive(true);
            }

        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during OnMessageReceived: " + ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
