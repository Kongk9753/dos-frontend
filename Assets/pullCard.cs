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


        if (pullTransform.parent.gameObject.name == "Cube1" || pullTransform.parent.gameObject.name == "Cube3")
        {
            pullTransform.position = transform.position + new Vector3( pullTransform.parent.position.x - pullTransform.position.x + (90f * childCount), 135f,pullTransform.parent.position.z - pullTransform.position.z);
            pullTransform.localScale = new Vector3(0.1098217f, 1.011981f, 0.01f);
            pullTransform.eulerAngles = new Vector3(-20, 90, 0);
        }
        else
        {
            pullTransform.position = transform.position + new Vector3(-300f - pullTransform.parent.position.x - pullTransform.position.x + (90f * childCount), 135f, pullTransform.parent.position.z - pullTransform.position.z);
            pullTransform.localScale = new Vector3(0.003998217f, 1.011981f, 0.1200001f);
            pullTransform.eulerAngles = new Vector3(0, 90, 40);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

