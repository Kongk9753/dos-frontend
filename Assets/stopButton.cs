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
    void Update()
    {

    }


    void OnMouseDown()
    {
        stopButton = GameObject.Find("StopButton");
        stopButton.active = false;
    }
}
