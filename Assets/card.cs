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
    public Transform pullCardTransform;
    private string choosenColor = "";
    private Button blue;
    private Button green;
    private Button yellow;
    private Button red;
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
        int childCount = playerHand.transform.childCount;
        GameObject newestCard = parentObj.transform.GetChild(parentObj.transform.childCount - 1).gameObject;
        string[] newestCardName = newestCard.transform.name.Split("_");
        string[] layedCardName = gameObject.transform.name.Split("_");
        WebSocketManager.Instance.playedOrPulled = true;

        stopButton = GameObject.Find("StopButton");
        if (stopButton.active)
        {
            Debug.Log(counter.Instance.count + "laycard counter");
            //If it is a special card
            if (layedCardName[1] == "Draw4(Clone)")
            {
                for (int i = parentObj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(parentObj.transform.GetChild(i).gameObject);
                }
                counter.Instance.pickColor.SetActive(true);
                blue = GameObject.Find("Blue").GetComponent<Button>();
                red = GameObject.Find("Red").GetComponent<Button>();
                yellow = GameObject.Find("Yellow").GetComponent<Button>();
                green = GameObject.Find("Green").GetComponent<Button>();
                blue.onClick.AddListener(() => getColor("Blue"));
                red.onClick.AddListener(() => getColor("Red"));
                yellow.onClick.AddListener(() => getColor("Yellow"));
                green.onClick.AddListener(() => getColor("Green"));
                childCount = playerHand.transform.childCount;
                counter.Instance.count++;
                counter.Instance.plus4 = 0;
                counter.Instance.plus2 = 0;
            }
            else if (layedCardName[1] == "Color(Clone)")
            {
                for (int i = parentObj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(parentObj.transform.GetChild(i).gameObject);
                }
                counter.Instance.pickColor.SetActive(true);
                blue = GameObject.Find("Blue").GetComponent<Button>();
                red = GameObject.Find("Red").GetComponent<Button>();
                yellow = GameObject.Find("Yellow").GetComponent<Button>();
                green = GameObject.Find("Green").GetComponent<Button>();
                blue.onClick.AddListener(() => getColorPicker("Blue"));
                red.onClick.AddListener(() => getColorPicker("Red"));
                yellow.onClick.AddListener(() => getColorPicker("Yellow"));
                green.onClick.AddListener(() => getColorPicker("Green"));
                childCount = playerHand.transform.childCount;
                counter.Instance.count++;
            }
            else if (newestCardName[1] == layedCardName[1])
            {
                for (int i = parentObj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(parentObj.transform.GetChild(i).gameObject);
                }
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                childCount = playerHand.transform.childCount;
                counter.Instance.count++;
            }
            else if (newestCardName[0] == layedCardName[0] && layedCardName[1] == "Skip(Clone)")
            {
                for (int i = parentObj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(parentObj.transform.GetChild(i).gameObject);
                }
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                childCount = playerHand.transform.childCount;
            }
            else if (newestCardName[0] == layedCardName[0] && layedCardName[1] == "Draw2(Clone)")
            {
                for (int i = parentObj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(parentObj.transform.GetChild(i).gameObject);
                }
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                childCount = playerHand.transform.childCount;
                counter.Instance.count++;
                counter.Instance.plus2 = 0;
            }
            //If the color of teh cards are equal to eachother and the numbers arent
            else if (newestCardName[0] == layedCardName[0] && newestCardName[1] != layedCardName[1] && counter.Instance.count == 0)
            {
                for (int i = parentObj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(parentObj.transform.GetChild(i).gameObject);
                }
                transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.parent = parentObj.transform;
                transform.localScale = new Vector3(100f, 0.1f, 150f);
                counter.Instance.count++;
                WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
                childCount = playerHand.transform.childCount;
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
    void getColor(string buttonName)
    {
        int childCount = playerHand.transform.childCount;
        choosenColor = buttonName;
        counter.Instance.pickColor.SetActive(false);
        transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.parent = parentObj.transform;
        transform.localScale = new Vector3(100f, 0.1f, 150f);
        counter.Instance.count++;
        gameObject.transform.name = choosenColor + "_Draw4(Clone)";
        childCount = playerHand.transform.childCount;
        counter.Instance.count++;
        counter.Instance.plus4 = 0;
        WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
        if (childCount == 0)
        {
            WebSocketManager.Instance.Send("win:" + WebSocketManager.Instance.player);
        }
    }

    void getColorPicker(string buttonName)
    {
        int childCount = playerHand.transform.childCount;
        choosenColor = buttonName;
        counter.Instance.pickColor.SetActive(false);
        transform.position = transform.position + new Vector3(0 - transform.position.x, 0 - transform.position.y - (1f * childCount), 1300 - transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.parent = parentObj.transform;
        transform.localScale = new Vector3(100f, 0.1f, 150f);
        counter.Instance.count++;
        gameObject.transform.name = choosenColor + "_Color(Clone)";
        childCount = playerHand.transform.childCount;
        WebSocketManager.Instance.Send("played:" + gameObject.transform.name);
        if (childCount == 0)
        {
            WebSocketManager.Instance.Send("win:" + WebSocketManager.Instance.player);
        }
    }
}
