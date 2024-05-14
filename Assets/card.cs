using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public GameObject parentObj;
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y, 1300 - transform.position.z);
        transform.eulerAngles = new Vector3(0, 90, 40);
        transform.parent = parentObj.transform;
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
