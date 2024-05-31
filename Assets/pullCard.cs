using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pullCard : MonoBehaviour
{
    public GameObject parentObj;
    private Transform meeple;
    private Animation anim;
    private string cubeNumber;
    public pullCard Instance;

    void OnMouseDown()
    {
        Pull(meeple, cubeNumber);
        WebSocketManager.Instance.playedOrPulled = true;

    }

    public void Pull(Transform pullTransform, string cubeNumber)
    {
        Debug.Log("OnMouseDown");
        parentObj = GameObject.Find(cubeNumber);
        int i = 0;
        if (parentObj == null)
        {
            Debug.LogError("Parent object not found: Cube" + cubeNumber);
            return;
        }

        // Check if the current transform has at least one child
        if (transform.childCount == 0)
        {
            Debug.LogError("Transform has no children.");
            return;
        }
        pullTransform = transform.GetChild(0);
        int childCount = parentObj.transform.childCount;
        Debug.Log("Child count: " + childCount);
        pullTransform.parent = parentObj.transform;

        // anim = pullTransform.GetComponent<Animation>();
        // anim.Play("pullCard");  

        pullTransform.position = transform.position + new Vector3(-300f - pullTransform.parent.position.x - pullTransform.position.x + (90f * childCount), 135f, pullTransform.parent.position.z - pullTransform.position.z);
        pullTransform.eulerAngles = new Vector3(0, 90, 40);
        pullTransform.localScale = new Vector3(0.003998217f, 1.011981f, 0.1200001f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

