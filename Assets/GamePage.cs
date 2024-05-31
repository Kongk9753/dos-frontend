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
        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 0)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube0;
            dictionary[WebSocketManager.Instance.players[1]] = cube1;
            dictionary[WebSocketManager.Instance.players[2]] = cube2;
            dictionary[WebSocketManager.Instance.players[3]] = cube3;
            pulls.Pull(card, "0");
            pulls.Pull(card, "1");
            pulls.Pull(card, "2");
            pulls.Pull(card, "3");

            Debug.Log("0");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 1)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube3;
            dictionary[WebSocketManager.Instance.players[1]] = cube0;
            dictionary[WebSocketManager.Instance.players[2]] = cube1;
            dictionary[WebSocketManager.Instance.players[3]] = cube2;
            pulls.Pull(card, "3");
            pulls.Pull(card, "0");
            pulls.Pull(card, "1");
            pulls.Pull(card, "2");
            Debug.Log("1");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 2)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube2;
            dictionary[WebSocketManager.Instance.players[1]] = cube3;
            dictionary[WebSocketManager.Instance.players[2]] = cube0;
            dictionary[WebSocketManager.Instance.players[3]] = cube1;
            pulls.Pull(card, "2");
            pulls.Pull(card, "3");
            pulls.Pull(card, "0");
            pulls.Pull(card, "1");
            Debug.Log("2");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 3)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube1;
            dictionary[WebSocketManager.Instance.players[1]] = cube2;
            dictionary[WebSocketManager.Instance.players[2]] = cube3;
            dictionary[WebSocketManager.Instance.players[3]] = cube0;
            pulls.Pull(card, "1");
            pulls.Pull(card, "2");
            pulls.Pull(card, "3");
            pulls.Pull(card, "0");
            Debug.Log("3");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
