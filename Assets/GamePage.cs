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

    private GameObject changecolor;

    private Text dosText;

    private Transform card;
    private pullCard pulls;
    public otherCard findCard;
    private GameObject stopButton;
    public Transform cards;
    public Vector3 rotationAngles; // Define the rotation angles here
    public GameObject deck;
    private Dictionary<string, string> dictionary;

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<string, string>();
        dosCanvas = GameObject.Find("Dos");
        deck = GameObject.Find("Deck");

        dosCanvas.SetActive(false);
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
            pulls.Pull(card, "Cube0", "false");
            pulls.Pull(card, "Cube1", "true");
            pulls.Pull(card, "Cube2", "true");
            pulls.Pull(card, "Cube3", "true");
            Debug.Log("0");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 1)
        {
            dictionary[WebSocketManager.Instance.players[0]] = "Cube3";
            dictionary[WebSocketManager.Instance.players[1]] = "Cube0";
            dictionary[WebSocketManager.Instance.players[2]] = "Cube1";
            dictionary[WebSocketManager.Instance.players[3]] = "Cube2";
            pulls.Pull(card, "Cube3", "true");
            pulls.Pull(card, "Cube0", "false");
            pulls.Pull(card, "Cube1", "true");
            pulls.Pull(card, "Cube2", "true");
            Debug.Log("1");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 2)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube2.name;
            dictionary[WebSocketManager.Instance.players[1]] = cube3.name;
            dictionary[WebSocketManager.Instance.players[2]] = cube0.name;
            dictionary[WebSocketManager.Instance.players[3]] = cube1.name;
            pulls.Pull(card, "Cube2", "true");
            pulls.Pull(card, "Cube3", "true");
            pulls.Pull(card, "Cube0", "false");
            pulls.Pull(card, "Cube1", "true");
            Debug.Log("2");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 3)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube1.name;
            dictionary[WebSocketManager.Instance.players[1]] = cube2.name;
            dictionary[WebSocketManager.Instance.players[2]] = cube3.name;
            dictionary[WebSocketManager.Instance.players[3]] = cube0.name;
            pulls.Pull(card, "Cube1", "true");
            pulls.Pull(card, "Cube2", "true");
            pulls.Pull(card, "Cube3", "true");
            pulls.Pull(card, "Cube0", "false");
            Debug.Log("3");
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
                Debug.Log("4");

                Debug.Log(command[1] + "player");
                Debug.Log("5");

                foreach (KeyValuePair<string, string> keyValue in dictionary)
                {
                    Debug.Log("6");

                    string key = keyValue.Key;
                    string value = keyValue.Value;

                    if (value == null)
                    {
                        Debug.LogError("GameObject value is null for key: " + key);
                        continue;
                    }
                    if (key == command[1] && command[1] != WebSocketManager.Instance.player)
                    {
                        findCard = GameObject.Find("Blue_2(Clone)").GetComponent<otherCard>();

                        findCard.LayOthersCard(value, command[2]);
                    }
                    Debug.Log("AccessDictionary - " + key + ": " + value + " eehehheh");
                }


            }
            else if (command[0] == "turn")
            {
                Debug.Log("myturn");
                stopButton.SetActive(true);

                GameObject newestCard = deck.transform.GetChild(deck.transform.childCount - 1).gameObject;
                string[] newestCardName = newestCard.transform.name.Split("_");

                if (newestCardName[0] == "ASpecial" && newestCardName[1] == "draw4(Clone)")
                {
                    WebSocketManager.Instance.lastCardPlus4 = true;
                }

            }
            else if (command[0] == "dos")
            {
                Debug.Log("myturn");
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


        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error during OnMessageReceived: " + ex.Message);
        }
    }


    private IEnumerator HidePopup(string dosPlayer)
    {
        dosText = GameObject.Find("DosText").GetComponent<Text>();
        dosText.text = dosPlayer + " Has DOS";

        // Wait for 2 seconds
        yield return new WaitForSeconds(5f);
        // Hide the popup
        dosCanvas.SetActive(false);
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
