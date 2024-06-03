using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dos : MonoBehaviour
{
    private pullCard pulls;
    private GameObject dosButton;
    private GameObject parentObj;
    private Transform card;
    void Start()
    {
        card = GameObject.Find("Cards").transform;
    }
    void OnMouseDown()
    {
        parentObj = GameObject.Find("Cube0");
        int childCount = parentObj.transform.childCount;

        if (childCount == 3)
        {
            dosButton = GameObject.Find("DosButton");
            dosButton.active = false;
            WebSocketManager.Instance.Send("dos:"+ WebSocketManager.Instance.player);
        }
        else
        {
            pulls.Pull(card, "Cube0", "false");
            WebSocketManager.Instance.playedOrPulled = false;
            pulls.Pull(card, "Cube0", "false");
            WebSocketManager.Instance.playedOrPulled = false;
        }
    }
}
