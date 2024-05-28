using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
public class start_page : MonoBehaviour
{
    public Text Lobby_Code;
    public Button Join_Lobby_Button;
    public Button Create_Lobby_Button;
    private IEnumerator coroutine;
    public string CreateLobbyCode;
    public static start_page Instance;


    // Start is called before the first frame update
    void Start()
    {
        Button joinLobby = Join_Lobby_Button.GetComponent<Button>();
        joinLobby.onClick.AddListener(Join_Lobby);
        Button createLobby = Create_Lobby_Button.GetComponent<Button>();
        createLobby.onClick.AddListener(Create_Lobby);

        Debug.Log(idToken.Instance.id + "startpage");
    }
    void Join_Lobby()
    {
        string LobbyCode = Lobby_Code.text.ToString();
        Debug.Log(LobbyCode);
        DestroyImmediate(Camera.main.gameObject);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Start_Page");
        WebSocketManager.Instance.Connect(LobbyCode, idToken.Instance.id);
        //WebSocketManager.Instance.OnMessageReceived += HandleMessage;
    }

    void HandleMessage(string message)
    {
        // Handle the received message here
        Debug.Log("Received message: " + message);
    }

    void Create_Lobby()
    {
        coroutine = GetRequest();
        StartCoroutine(coroutine);
        Debug.Log(start_page.Instance.CreateLobbyCode + " code");

    }

    IEnumerator GetRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/game/create"))
        {
            www.SetRequestHeader("Authorization", idToken.Instance.id);
            yield return www.SendWebRequest();
            Debug.Log(www.result + " result");
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("halloooo");
                Debug.Log("Received: " + www.downloadHandler.text);
                string json = www.downloadHandler.text;
                start_page.Instance.CreateLobbyCode = (string)JToken.Parse(json).SelectToken("code");
                WebSocketManager.Instance.Connect(start_page.Instance.CreateLobbyCode, idToken.Instance.id);
                SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
                SceneManager.UnloadScene("Start_Page");
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        } // The using block ensures www.Dispose() is called when this block is exited
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
