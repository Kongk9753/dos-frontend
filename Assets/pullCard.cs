using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pullCard : MonoBehaviour
{
    public GameObject parentObj;
    private Transform meeple;
    private string cubeNumber;
    public Vector3 rotationAngles;
    public Transform cards;

    void OnMouseDown()
    {
        cubeNumber = gameObject.name;
        Pull(meeple, "Cube0", "false");
        WebSocketManager.Instance.playedOrPulled = true;
        WebSocketManager.Instance.Send("card:pull");
    }

    public void Pull(Transform pullTransform, string cubeNumber, string behind)
    {
        parentObj = GameObject.Find(cubeNumber);
        int i = 0;
        GameObject prefab;
        if (behind == "false")
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("test");
            int random = Random.Range(0, 6);
            prefab = prefabs[random];
        }
        else
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("behind");
            prefab = prefabs[0];
        }
        GameObject instance = Instantiate(prefab, cards.position + Vector3.right * i, Quaternion.identity, cards);


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
        // Rotate the instantiated prefab
        instance.transform.Rotate(rotationAngles);
        // Adjust the scale of the instantiated prefab if needed
        instance.transform.localScale = new Vector3(100f, 0.1f, 150f); // Set the scale to desired values


        pullTransform = transform.GetChild(0);
        int childCount = parentObj.transform.childCount;
        pullTransform.parent = parentObj.transform;

        if (pullTransform.parent.gameObject.name == "Cube1" || pullTransform.parent.gameObject.name == "Cube3")
        {
            pullTransform.position = transform.position + new Vector3(pullTransform.parent.position.x - pullTransform.position.x, 135f, -300f+ pullTransform.parent.position.z - pullTransform.position.z + (90f * childCount));
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
}

