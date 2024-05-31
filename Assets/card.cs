using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject stopButton;
    public GameObject playerHand;
    private Transform meeple;
    public pullCard pulls;
    private GameObject dosButton;
    private bool test;
    public Transform pullCardTransform;
    private Animation anim;
    private GameObject cards;

    public Vector3 rotationAngles; // Define the rotation angles here
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        LayCard();
    }

    public void LayOthersCard()
    {
        string parentName = transform.parent.gameObject.name;
        parentObj = GameObject.Find("Deck");
        playerHand = GameObject.Find(parentName);
        Debug.Log("OnMouseDown");
        int childCount = playerHand.transform.childCount;

        transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y + (1f * childCount), 1300 - transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.parent = parentObj.transform;
        transform.localScale = new Vector3(100f, 0.1f, 150f);
        return;
    }

    public void LayCard()
    {
        string parentName = transform.parent.gameObject.name;
        parentObj = GameObject.Find("Deck");
        playerHand = GameObject.Find(parentName);
        Debug.Log("OnMouseDown");
        int childCount = playerHand.transform.childCount;
        GameObject newestCard = parentObj.transform.GetChild(parentObj.transform.childCount - 1).gameObject;
        string[] newestCardName = newestCard.transform.name.Split("_");
        string[] layedCardName = gameObject.transform.name.Split("_");


        stopButton = GameObject.Find("StopButton");
        if (stopButton.active)
        {
            Debug.Log(counter.Instance.count + "count");
            Debug.Log(newestCardName[1] + layedCardName[1] + "cards");
            //If the number on the cards are equal to each other
            if (newestCardName[1] == layedCardName[1])
            {
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                Debug.Log("send1");
            }
            //If the color of teh cards are equal to eachother and the numbers arent
            else if (newestCardName[0] == layedCardName[0] && newestCardName[1] != layedCardName[1] && counter.Instance.count == 0)
            {
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                counter.Instance.count++;
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                Debug.Log("send2");
            }
            //If it is a special card
            else
            {
                if (layedCardName[1] == "4")
                {
                    Debug.Log("draw 4 Cards");
                }
                gameObject.transform.name = "Green_Color";
                Debug.Log("send3");

            }

        }
        else
        {
            Debug.Log("Next person");
        }



        // anim = gameObject.GetComponent<Animation>();
        // anim.Play("layCard");
        //test =GameObject.Find("Blue_2(Clone)").GetComponent<Animation>().Play("layCard");


        //Sort cards in hand when card has been layed doen
        for (int i = 0; i < childCount; i++)
        {
            meeple = playerHand.transform.GetChild(i);
            meeple.position = meeple.position + new Vector3(-300f - meeple.parent.position.x - meeple.position.x + (90f * i), 0, meeple.parent.position.z - meeple.position.z);

        } // Set the scale to desired values

        dosButton = GameObject.Find("DosButton");
        if (dosButton.active && childCount == 2)
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
