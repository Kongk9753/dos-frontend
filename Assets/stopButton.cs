using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{

    private GameObject stopButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame


    void OnMouseDown()
    {
        stopButton = GameObject.Find("StopButton");
        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == WebSocketManager.Instance.players.Count - 1)
        {
            string firstElement = WebSocketManager.Instance.players[0];
            WebSocketManager.Instance.Send("turn:" + firstElement);
        }
        else
        {
            string element = WebSocketManager.Instance.players[WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) + 1];
            WebSocketManager.Instance.Send("turn:" + element);
        }
        stopButton.active = false;
    }

}
