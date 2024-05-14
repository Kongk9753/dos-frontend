using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullCard : MonoBehaviour
{
    public GameObject parentObj;
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        transform.parent.position = transform.position + new Vector3(-1500 + transform.parent.position.x, 30 + transform.parent.position.y, -875 + transform.parent.position.z);
        transform.parent.eulerAngles = new Vector3(0, 90, 90);
        transform.parent = parentObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
