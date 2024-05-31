using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dos : MonoBehaviour
{
    private pullCard pulls;
    private GameObject dosButton;
    public Transform pullCardTransform;
    private GameObject parentObj;
    private Transform card;

    void Start()
    {
        card = GameObject.Find("Cards").transform;

    }
    void OnMouseDown()
    {

        parentObj = GameObject.Find("Cube");
        int childCount = parentObj.transform.childCount;

        if (childCount == 3)
        {
            dosButton = GameObject.Find("DosButton");
            dosButton.active = false;
            WebSocketManager.Instance.Send("dos:"+ WebSocketManager.Instance.player);
        }
        else
        {
            pulls.Pull(card, "Cube0");
            WebSocketManager.Instance.playedOrPulled = false;
            pulls.Pull(card, "Cube0");
            WebSocketManager.Instance.playedOrPulled = false;
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}
