using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    private GameObject stopButton;
    private pullCard pulls;
    private Transform card;
    public GameObject deck;

    public StopButton Instance;
    // Start is called before the first frame update
    void Start()
    {
        pulls = GameObject.Find("Cards").GetComponent<pullCard>();
        card = GameObject.Find("Cards").transform;
    }
    void OnMouseDown()
    {
        if (counter.Instance.plus4 > 0)
        {
            for (int i = 0; i < 4 * counter.Instance.plus4; i++)
            {
                pulls.Pull(card, "Cube0", "false");
            }
            counter.Instance.plus4 = 0;
            WebSocketManager.Instance.Send("card:reset");
        }

        if (counter.Instance.plus2 > 0)
        {
            for (int i = 0; i < 2 * counter.Instance.plus2; i++)
            {
                pulls.Pull(card, "Cube0", "false");
            }
            counter.Instance.plus2 = 0;
            WebSocketManager.Instance.Send("card:reset");
        }

        if (!WebSocketManager.Instance.playedOrPulled)
        {
            pulls.Pull(card, "Cube0", "false");
            WebSocketManager.Instance.playedOrPulled = false;
            pulls.Pull(card, "Cube0", "false");
            WebSocketManager.Instance.playedOrPulled = false;
        }
        nextPlayer(0, 1);

    }

    public void nextPlayer(int player1, int player2)
    {
        counter.Instance.count = 0;
        stopButton = GameObject.Find("StopButton");
        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == WebSocketManager.Instance.players.Count - 1)
        {
            string firstElement = WebSocketManager.Instance.players[player1];
            WebSocketManager.Instance.Send("turn:" + firstElement);
        }
        else
        {
            string element = WebSocketManager.Instance.players[WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) + player2];
            if (element == "dummy")
            {
                element = WebSocketManager.Instance.players[player1];

            }
            WebSocketManager.Instance.Send("turn:" + element);
        }
        stopButton.active = false;
    }

}
