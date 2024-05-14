using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class start_page : MonoBehaviour
{
    public Text Lobby_Code;
    public Button Join_Lobby_Button;
    public Button Create_Lobby_Button;
    // Start is called before the first frame update
    void Start()
    {
        Button joinLobby = Join_Lobby_Button.GetComponent<Button>();
		joinLobby.onClick.AddListener(Join_Lobby);
        Button createLobby = Create_Lobby_Button.GetComponent<Button>();
		createLobby.onClick.AddListener(Create_Lobby);
    }
    void Join_Lobby()
    {
        string LobbyCode = Lobby_Code.text.ToString();
        Debug.Log(LobbyCode);
        DestroyImmediate(Camera.main.gameObject);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Start_Page");
    }

    void Create_Lobby()
    {
        DestroyImmediate(Camera.main.gameObject);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Start_Page");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
