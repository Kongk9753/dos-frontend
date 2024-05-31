using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class Lobby : MonoBehaviour
{
    private Text[] playerNames = new Text[10];
    private Text title;
    public GameObject StartGame_Button;
    void Start()
    {
        Button startGame = StartGame_Button.GetComponent<Button>();
        startGame.onClick.AddListener(StartGameClick);

        try
        {
            playerNames[0] = GameObject.Find("PlayerName").GetComponent<Text>();
            playerNames[1] = GameObject.Find("PlayerName (1)").GetComponent<Text>();
            playerNames[2] = GameObject.Find("PlayerName (2)").GetComponent<Text>();
            playerNames[3] = GameObject.Find("PlayerName (3)").GetComponent<Text>();
            // Initialize other playerNames if necessary

            title = GameObject.Find("Title").GetComponent<Text>();
            title.text = "Lobby: " + start_page.Instance.CreateLobbyCode;

            // Subscribe to WebSocketManager's OnMessage event
            WebSocketManager.Instance.OnMessageReceived += (string message) =>
            {
                UnityMainThreadDispatcher.Dispatcher.Enqueue(() => OnMessageReceived(message));
            };

            Debug.Log("UI elements initialized and WebSocket event subscribed.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during Start initialization: " + ex.Message);
        }
    }


    void StartGameClick()
    {

        WebSocketManager.Instance.Send("game:start");
    }

    private void OnMessageReceived(string message)
    {
        try
        {
            string[] command = message.Split(':');
            Debug.Log("Received message: " + message);
            if (command.Length < 2)
            {
                Debug.LogError("Invalid message format");
                return;
            }

            Debug.Log("Command: " + command[0]);
            Debug.Log("Player Name: " + command[1]);
            Debug.Log("Code: " + start_page.Instance.CreateLobbyCode);

            if (command[0] == "joined" && command[1] != "")
            {
                WebSocketManager.Instance.players.Add(command[1]);
                UpdatePlayerNames();
            }
            else if (command[1] == "list")
            {
                List<string> formerPlayers = new List<string>();
                formerPlayers.AddRange(command[2].Split(","));
                Debug.Log(formerPlayers + "players");
                for (int i = 0; i < formerPlayers.Count; i++)
                {
                    if (formerPlayers[i] != "")
                    {
                        WebSocketManager.Instance.players.Add(formerPlayers[i]);
                    }
                }
                UpdatePlayerNames();
            }
            else if (command[1] == "started")
            {
                LoadScene("Game_page");

            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during OnMessageReceived: " + ex.Message);
        }
    }

    private void UpdatePlayerNames()
    {
        try
        {
            for (int i = 0; i < WebSocketManager.Instance.players.Count && i < playerNames.Length; i++)
            {
                if (playerNames[i] != null)
                {
                    playerNames[i].text = WebSocketManager.Instance.players[i];
                }
                else
                {
                    Debug.LogWarning("playerNames[" + i + "] is null");
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during UpdatePlayerNames: " + ex.Message);
        }
    }

    public void LoadScene(string sceneName)
    {
        // Start loading the new scene
        StartCoroutine(LoadAndUnloadScene(sceneName));
    }


    private IEnumerator LoadAndUnloadScene(string sceneName)
    {
        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Wait until the new scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Set the newly loaded scene as the active scene
        Scene newScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(newScene);

        // Unload the first scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("Login");
        AsyncOperation asyncUnload2 = SceneManager.UnloadSceneAsync("Lobby");

        // Wait until the first scene is fully unloaded
        while (!asyncUnload.isDone && !asyncUnload2.isDone)
        {
            yield return null;
        }
    }

}
