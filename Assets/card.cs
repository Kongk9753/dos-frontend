using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject playerHand;
    private Transform meeple;
    public pullCard pulls;
    private GameObject dosButton;
    private bool test;
    public Transform pullCardTransform;
    private Animation anim;


    //private GameObject dosButton;

    public Vector3 rotationAngles; // Define the rotation angles here
    void OnMouseDown()
    {
        parentObj = GameObject.Find("Deck");
        playerHand = GameObject.Find("Cube");
        Debug.Log("OnMouseDown");
        anim = gameObject.GetComponent<Animation>();
        anim.Play("layCard");  
       // transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y, 1300 - transform.position.z);            
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.parent = parentObj.transform;
        transform.localScale = new Vector3(100f, 0.1f, 150f); 
        //test =GameObject.Find("Blue_2(Clone)").GetComponent<Animation>().Play("layCard");

        int childCount = playerHand.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            meeple = playerHand.transform.GetChild(i);
            meeple.position = meeple.position + new Vector3(-300f-meeple.parent.position.x-meeple.position.x +(90f*i),0,meeple.parent.position.z-meeple.position.z);

        } // Set the scale to desired values

        dosButton = GameObject.Find("DosButton");
        if (dosButton.active && childCount == 2)
        {            
            pulls = GameObject.Find("Cards").GetComponent<pullCard>();
            pulls.Pull(pullCardTransform);
            pulls.Pull(pullCardTransform);
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
