using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winscreen : MonoBehaviour
{

    private Text WinText;
    // Start is called before the first frame update
    void Start()
    {
        WinText = GameObject.Find("Winner").GetComponent<Text>();
        WinText.text = "Winner: " + WebSocketManager.Instance.winner; 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
