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
        start_page.Instance.CreateLobbyCode = LobbyCode;
        Debug.Log(LobbyCode);
        DestroyImmediate(Camera.main.gameObject);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Start_Page");
        WebSocketManager.Instance.Connect(LobbyCode, idToken.Instance.id);
    }

    void Create_Lobby()
    {
        coroutine = GetRequest();
        StartCoroutine(coroutine);

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
                string json = www.downloadHandler.text;
                start_page.Instance.CreateLobbyCode = (string)JToken.Parse(json).SelectToken("code");
                WebSocketManager.Instance.Connect(start_page.Instance.CreateLobbyCode, idToken.Instance.id);
                WebSocketManager.Instance.isOwner = true;
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
