using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck : MonoBehaviour
{
    public pullCard pulls;
    public Transform pullCardTransform;

    void Start()
    {
        pulls = GameObject.Find("Cards").GetComponent<pullCard>();

    }
    void OnMouseDown()
    {
        pulls.Pull(pullCardTransform, "Cube0", "false");

    }
}