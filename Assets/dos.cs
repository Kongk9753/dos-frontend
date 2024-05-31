using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dos : MonoBehaviour
{
    private pullCard pulls;
    private GameObject dosButton;
    public Transform pullCardTransform;
    private GameObject parentObj;

    void OnMouseDown()
    {

        parentObj = GameObject.Find("Cube");
        int childCount = parentObj.transform.childCount;

        if (childCount == 3)
        {
            dosButton = GameObject.Find("DosButton");
            dosButton.active = false;
        }
        else
        {
            // pulls = GameObject.Find("Cards").GetComponent<pullCard>();
            // pulls.Pull(pullCardTransform);
            // pulls.Pull(pullCardTransform);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
