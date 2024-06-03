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
    private Transform cards;
    private string choosenColor = "";
    public card Instance;

    private Button blue;
    private Button green;
    private Button yellow;
    private Button red;

    public Vector3 rotationAngles; // Define the rotation angles here
    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        LayCard();
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
        WebSocketManager.Instance.playedOrPulled = true;

        stopButton = GameObject.Find("StopButton");
        if (stopButton.active)
        {
            Debug.Log(layedCardName[1] + " layed Card");
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
                childCount = playerHand.transform.childCount;
            }
            //If the color of teh cards are equal to eachother and the numbers arent
            else if (newestCardName[0] == layedCardName[0] && newestCardName[1] != layedCardName[1] && counter.Instance.count == 0 && newestCardName[1] != "draw4")
            {
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                counter.Instance.count++;
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                childCount = playerHand.transform.childCount;
            }
            //If it is a special card
            else if (layedCardName[1] == "draw4(Clone)")
            {
                counter.Instance.pickColor.SetActive(true);
                blue = GameObject.Find("Blue").GetComponent<Button>();
                red = GameObject.Find("Red").GetComponent<Button>();
                yellow = GameObject.Find("Yellow").GetComponent<Button>();
                green = GameObject.Find("Green").GetComponent<Button>();
                blue.onClick.AddListener(getColor);
                red.onClick.AddListener(getColor);
                yellow.onClick.AddListener(getColor);
                green.onClick.AddListener(getColor);
            }
            else if (layedCardName[1] == "Color(Clone)")
            {
                counter.Instance.pickColor.SetActive(true);
                blue = GameObject.Find("Blue").GetComponent<Button>();
                red = GameObject.Find("Red").GetComponent<Button>();
                yellow = GameObject.Find("Yellow").GetComponent<Button>();
                green = GameObject.Find("Green").GetComponent<Button>();
                blue.onClick.AddListener(getColorPicker);
                red.onClick.AddListener(getColorPicker);
                yellow.onClick.AddListener(getColorPicker);
                green.onClick.AddListener(getColorPicker);
            }

            if (childCount == 0)
            {
                Debug.Log("done");
                WebSocketManager.Instance.Send("win:" + WebSocketManager.Instance.player);
            }

        }
        else
        {
            Debug.Log("Next person");
        }
        //Sort cards in hand when card has been layed doen
        for (int i = 0; i < childCount; i++)
        {
            meeple = playerHand.transform.GetChild(i);
            meeple.position = meeple.position + new Vector3(-300f - meeple.parent.position.x - meeple.position.x + (90f * i), 0, meeple.parent.position.z - meeple.position.z);

        } // Set the scale to desired values

        dosButton = GameObject.Find("DosButton");
        if (dosButton.active && childCount == 2)
        {
            pulls = GameObject.Find("Cards").GetComponent<pullCard>();
            pulls.Pull(pullCardTransform, "Cube0", "false");
            pulls.Pull(pullCardTransform, "Cube0", "false");
        }
    }


    void getColor()
    {
        int childCount = playerHand.transform.childCount;
        string buttonName = gameObject.name;
        choosenColor = buttonName;
        counter.Instance.pickColor.SetActive(false);
        WebSocketManager.Instance.lastCardPlus4 = false;
        transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.parent = parentObj.transform;
        transform.localScale = new Vector3(100f, 0.1f, 150f);
        counter.Instance.count++;
        gameObject.transform.name = choosenColor + "_draw4(Clone)";
        childCount = playerHand.transform.childCount;
        WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
    }

     void getColorPicker()
    {
        int childCount = playerHand.transform.childCount;
        string buttonName = gameObject.name;
        choosenColor = buttonName;
        counter.Instance.pickColor.SetActive(false);
        transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.parent = parentObj.transform;
        transform.localScale = new Vector3(100f, 0.1f, 150f);
        counter.Instance.count++;
        gameObject.transform.name = choosenColor + "_Color";
        childCount = playerHand.transform.childCount;
        WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
    }

    // Start is called before the first frame update
}
