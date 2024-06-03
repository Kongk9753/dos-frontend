using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{

    private GameObject stopButton;
    private pullCard pulls;
    private Transform card;


    // Start is called before the first frame update
    void Start()
    {
        pulls = GameObject.Find("Cards").GetComponent<pullCard>();
        card = GameObject.Find("Cards").transform;

    }
    void OnMouseDown()
    {
        if (WebSocketManager.Instance.lastCardPlus4)
        {
            pulls.Pull(card, "Cube0", "false");
            pulls.Pull(card, "Cube0", "false");
            pulls.Pull(card, "Cube0", "false");
            pulls.Pull(card, "Cube0", "false");
            //Later finifh when other players get message that you pull card
        }

        if (!WebSocketManager.Instance.playedOrPulled)
        {
            pulls.Pull(card, "Cube0", "false");
            WebSocketManager.Instance.playedOrPulled = false;
            pulls.Pull(card, "Cube0", "false");
            WebSocketManager.Instance.playedOrPulled = false;

        }
        stopButton = GameObject.Find("StopButton");
        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == WebSocketManager.Instance.players.Count - 1)
        {
            string firstElement = WebSocketManager.Instance.players[0];
            WebSocketManager.Instance.Send("turn:" + firstElement);
        }
        else
        {
            string element = WebSocketManager.Instance.players[WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) + 1];
            if (element == "dummy")
            {
                element = WebSocketManager.Instance.players[0];

            }
            WebSocketManager.Instance.Send("turn:" + element);
        }
        stopButton.active = false;
    }

}
