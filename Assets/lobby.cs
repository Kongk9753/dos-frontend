using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class lobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to WebSocketManager's OnMessage event
        WebSocketManager.Instance.OnMessageReceived += HandleMessage;


          // Find the Panel GameObject
        GameObject panel = GameObject.Find("Panel");
        if (panel != null)
        {
            // Create a new GameObject and set its parent to the Panel
            GameObject newTextGO = new GameObject("myTextGO");
            newTextGO.transform.SetParent(panel.transform, false); // Ensure that the scale is not affected by the parent's scale

            // Add the Text component to the new GameObject
            Text newText = newTextGO.AddComponent<Text>();
            newText.text = "Ta-dah!";

            // Set the RectTransform properties for positioning and size
            RectTransform rectTransform = newText.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero; // Set the position to the center of the panel
            rectTransform.sizeDelta = new Vector2(200, 50); // Set the size of the text box
        }
        else
        {
            Debug.LogError("Panel GameObject not found!");
        }
    }

    void HandleMessage(string message)
    {
        Debug.Log("Received message: " + message);

        // Find the Panel GameObject
        GameObject panel = GameObject.Find("Panel");
        if (panel != null)
        {
            // Create a new GameObject and set its parent to the Panel
            GameObject newTextGO = new GameObject("myTextGO");
            newTextGO.transform.SetParent(panel.transform, false); // Ensure that the scale is not affected by the parent's scale

            // Add the Text component to the new GameObject
            Text newText = newTextGO.AddComponent<Text>();
            newText.text = "Ta-dah!";

            // Set the RectTransform properties for positioning and size
            RectTransform rectTransform = newText.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero; // Set the position to the center of the panel
            rectTransform.sizeDelta = new Vector2(200, 50); // Set the size of the text box
        }
        else
        {
            Debug.LogError("Panel GameObject not found!");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
