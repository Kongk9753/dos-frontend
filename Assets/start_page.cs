using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using WebSocketSharp;
public class start_page : MonoBehaviour
{
    public Text Lobby_Code;
    public Button Join_Lobby_Button;
    public Button Create_Lobby_Button;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        Button joinLobby = Join_Lobby_Button.GetComponent<Button>();
        joinLobby.onClick.AddListener(Join_Lobby);
        Button createLobby = Create_Lobby_Button.GetComponent<Button>();
        createLobby.onClick.AddListener(Create_Lobby);
        Debug.Log("start");
        coroutine = GetRequest();
        StartCoroutine(coroutine);
    }
    void Join_Lobby()
    {
        string LobbyCode = Lobby_Code.text.ToString();
        Debug.Log(LobbyCode);
        DestroyImmediate(Camera.main.gameObject);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Start_Page");
        WebSocketManager.Instance.Connect(LobbyCode);
        //WebSocketManager.Instance.OnMessageReceived += HandleMessage;
    }

    void HandleMessage(string message)
    {
        // Handle the received message here
        Debug.Log("Received message: " + message);
    }

    void Create_Lobby()
    {
        DestroyImmediate(Camera.main.gameObject);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Start_Page");
    }

    IEnumerator GetRequest()
    {
        Debug.Log("GetRequest");
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/game/create"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Received: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        } // The using block ensures www.Dispose() is called when this block is exited

    }


}
