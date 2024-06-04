using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePage : MonoBehaviour
{
    private GameObject cube0;
    private GameObject cube1;
    private GameObject cube2;
    private GameObject cube3;
    private GameObject dosCanvas;
    private GameObject colorCanvas;
    private Text dosText;
    private Text colorText;
    private Transform card;
    private pullCard pulls;
    public otherCard findCard;
    private GameObject stopButton;
    private StopButton stopButtonFunc;

    public GameObject deck;
    private Dictionary<string, string> dictionary;

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<string, string>();
        dosCanvas = GameObject.Find("Dos");
        colorCanvas = GameObject.Find("Color");
        deck = GameObject.Find("Deck");
        counter.Instance.pickColor.SetActive(false);
        dosCanvas.SetActive(false);
        colorCanvas.SetActive(false);
        while (WebSocketManager.Instance.players.Count < 4)
        {
            WebSocketManager.Instance.players.Add("dummy");
        }
        pulls = GameObject.Find("Cards").GetComponent<pullCard>();
        card = GameObject.Find("Cards").transform;

        stopButton = GameObject.Find("StopButton");
        stopButton.SetActive(false);
        GameObject[] prefabs = Resources.LoadAll<GameObject>("behind");
        GameObject prefab = prefabs[0];

        WebSocketManager.Instance.Send("ready:now");

        cube0 = GameObject.Find("cube0");
        cube1 = GameObject.Find("cube1");
        cube2 = GameObject.Find("cube2");
        cube3 = GameObject.Find("cube3");


        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 0)
        {
            dictionary[WebSocketManager.Instance.players[0]] = "Cube0";
            dictionary[WebSocketManager.Instance.players[1]] = "Cube1";
            dictionary[WebSocketManager.Instance.players[2]] = "Cube2";
            dictionary[WebSocketManager.Instance.players[3]] = "Cube3";
            for (int i = 0; i < 7; i++)
            {
                pulls.Pull(card, "Cube0", "false");
                pulls.Pull(card, "Cube1", "true");
                pulls.Pull(card, "Cube2", "true");
                pulls.Pull(card, "Cube3", "true");
            }

        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 1)
        {
            dictionary[WebSocketManager.Instance.players[0]] = "Cube3";
            dictionary[WebSocketManager.Instance.players[1]] = "Cube0";
            dictionary[WebSocketManager.Instance.players[2]] = "Cube1";
            dictionary[WebSocketManager.Instance.players[3]] = "Cube2";
            for (int i = 0; i < 7; i++)
            {
                pulls.Pull(card, "Cube3", "true");
                pulls.Pull(card, "Cube0", "false");
                pulls.Pull(card, "Cube1", "true");
                pulls.Pull(card, "Cube2", "true");
            }

        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 2)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube2.name;
            dictionary[WebSocketManager.Instance.players[1]] = cube3.name;
            dictionary[WebSocketManager.Instance.players[2]] = cube0.name;
            dictionary[WebSocketManager.Instance.players[3]] = cube1.name;
            for (int i = 0; i < 7; i++)
            {
                pulls.Pull(card, "Cube2", "true");
                pulls.Pull(card, "Cube3", "true");
                pulls.Pull(card, "Cube0", "false");
                pulls.Pull(card, "Cube1", "true");
            }

        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 3)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube1.name;
            dictionary[WebSocketManager.Instance.players[1]] = cube2.name;
            dictionary[WebSocketManager.Instance.players[2]] = cube3.name;
            dictionary[WebSocketManager.Instance.players[3]] = cube0.name;
            for (int i = 0; i < 7; i++)
            {
                pulls.Pull(card, "Cube1", "true");
                pulls.Pull(card, "Cube2", "true");
                pulls.Pull(card, "Cube3", "true");
                pulls.Pull(card, "Cube0", "false");
            }

        }
        WebSocketManager.Instance.OnMessageReceived += (string message) =>
        {
            UnityMainThreadDispatcher.Dispatcher.Enqueue(() => OnMessageReceived(message));
        };
    }

    private void OnMessageReceived(string message)
    {
        try
        {
            string[] command = message.Split(':');
            Debug.Log("Received message: " + message);

            if (command[0] == "played" && command[1] != "")
            {
                if (command[2].Split("_")[1] == "Color(Clone)" || command[2].Split("_")[1] == "Draw4(Clone)")
                {
                    colorCanvas.SetActive(true);
                    string newColor = command[2].Split("_")[0];
                    StartCoroutine(HidePopupColor(newColor));
                }
                if (command[2].Split("_")[1] == "Draw4(Clone)" && command[1] != WebSocketManager.Instance.player)
                {
                    counter.Instance.plus4++;
                }
                if (command[2].Split("_")[1] == "Draw2(Clone)" && command[1] != WebSocketManager.Instance.player)
                {
                    counter.Instance.plus2++;
                }
                if (command[2].Split("_")[1] == "Skip(Clone)")
                {
                    stopButtonFunc.Instance.nextPlayer(1, 2);
                    stopButton.SetActive(false);
                }

                foreach (KeyValuePair<string, string> keyValue in dictionary)
                {

                    string key = keyValue.Key;
                    string value = keyValue.Value;

                    if (value == null)
                    {
                        Debug.LogError("GameObject value is null for key: " + key);
                        continue;
                    }
                    if (key == command[1] && command[1] != WebSocketManager.Instance.player)
                    {
                        findCard = GameObject.Find("Blue_2(Clone) (1)").GetComponent<otherCard>();
                        findCard.LayOthersCard(value, command[2]);
                    }
                }
            }
            else if (command[0] == "turn")
            {
                Debug.Log("myturn");
                stopButton.SetActive(true);
                GameObject newestCard = deck.transform.GetChild(deck.transform.childCount - 1).gameObject;
                string[] newestCardName = newestCard.transform.name.Split("_");

                if (newestCardName[1] == "Draw4(Clone)")
                {
                    counter.Instance.count++;
                }

            }
            else if (command[0] == "dos")
            {
                dosCanvas.SetActive(true);
                StartCoroutine(HidePopup(command[1]));
            }
            else if (command[0] == "won")
            {
                WebSocketManager.Instance.winner = command[1];
                LoadScene();
            }
            else if (command[0] == "pulled")
            {
                foreach (KeyValuePair<string, string> keyValue in dictionary)
                {
                    string key = keyValue.Key;
                    string value = keyValue.Value;

                    if (value == null)
                    {
                        Debug.LogError("GameObject value is null for key: " + key);
                        continue;
                    }
                    if (key == command[1] && command[1] != WebSocketManager.Instance.player)
                    {
                        pulls.Pull(card, value, "true");
                    }
                }


            }
            else if (command[0] == "count")
            {
                counter.Instance.plus4 = 0;
            }


        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during OnMessageReceived: " + ex.Message);
        }
    }


    private IEnumerator HidePopup(string dosPlayer)
    {
        dosText = GameObject.Find("DosText").GetComponent<Text>();
        dosText.text = dosPlayer + " has DOS";

        // Wait for 2 seconds
        yield return new WaitForSeconds(5f);
        // Hide the popup
        dosCanvas.SetActive(false);
    }

    private IEnumerator HidePopupColor(string color)
    {
        colorText = GameObject.Find("ColorText").GetComponent<Text>();
        colorText.text = color + " is the color";

        // Wait for 2 seconds
        yield return new WaitForSeconds(5f);
        // Hide the popup
        colorCanvas.SetActive(false);
    }

    public void LoadScene()
    {
        // Start loading the new scene
        StartCoroutine(LoadAndUnloadScene("WinScreen"));
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
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("Game_Page");

        // Wait until the first scene is fully unloaded
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
