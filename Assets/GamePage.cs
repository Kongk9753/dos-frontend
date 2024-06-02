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
    private Text dosText;

    private Transform card;
    private pullCard pulls;
    public card findCard;
    private GameObject stopButton;
    public Transform cards;
    public Vector3 rotationAngles; // Define the rotation angles here

    // Start is called before the first frame update
    void Start()
    {
        dosCanvas = GameObject.Find("Dos");
        dosCanvas.SetActive(false);
        while (WebSocketManager.Instance.players.Count < 4)
        {
            WebSocketManager.Instance.players.Add("dummy");
        }
        Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
        pulls = GameObject.Find("Cards").GetComponent<pullCard>();
        card = GameObject.Find("Cards").transform;

        stopButton = GameObject.Find("StopButton");
        stopButton.SetActive(false);
        GameObject[] prefabs = Resources.LoadAll<GameObject>("behind");
        GameObject prefab = prefabs[0];
        //GameObject behind = Instantiate(prefab, cards.position + Vector3.right * i, Quaternion.identity, cards);

        // // Rotate the instantiated prefab
        // behind.transform.Rotate(rotationAngles);

        // // Adjust the scale of the instantiated prefab if needed
        // behind.transform.localScale = new Vector3(100f, 0.1f, 150f);
        if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 0)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube0;
            dictionary[WebSocketManager.Instance.players[1]] = cube1;
            dictionary[WebSocketManager.Instance.players[2]] = cube2;
            dictionary[WebSocketManager.Instance.players[3]] = cube3;
            // for (int i = 0; i < WebSocketManager.Instance.players.Count; i++)
            // {
            //     Debug.Log("Pulling cards: " + WebSocketManager.Instance.players[i]);
            //     if (WebSocketManager.Instance.players[i] == "dummy")
            //     {
            //         continue;
            //     }

            //     dictionary[WebSocketManager.Instance.players[i]] = cards[i];
            //     for (int j = 0; j < 7; j++)
            //     {
            //         pulls.Pull(card, cards[i].ToString());
            //     }
            // }

            for (int i = 0; i < 3; i++)
            {
                pulls.Pull(card, "Cube0");
            }

            for (int i = 0; i < 7; i++)
            {

            }

            // for (int i = 0; i < 7; i++)
            // {
            //     pulls.Pull(card, "Cube0");
            //     pulls.Pull(<card>, "Cube1");
            //     pulls.Pull(card, "Cube2");
            //     pulls.Pull(card, "Cube3");
            // }
            Debug.Log("0");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 1)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube3;
            dictionary[WebSocketManager.Instance.players[1]] = cube0;
            dictionary[WebSocketManager.Instance.players[2]] = cube1;
            dictionary[WebSocketManager.Instance.players[3]] = cube2;
            pulls.Pull(card, "Cube3");
            pulls.Pull(card, "Cube0");
            pulls.Pull(card, "Cube1");
            pulls.Pull(card, "Cube2");
            Debug.Log("1");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 2)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube2;
            dictionary[WebSocketManager.Instance.players[1]] = cube3;
            dictionary[WebSocketManager.Instance.players[2]] = cube0;
            dictionary[WebSocketManager.Instance.players[3]] = cube1;
            pulls.Pull(card, "Cube2");
            pulls.Pull(card, "Cube3");
            pulls.Pull(card, "Cube0");
            pulls.Pull(card, "Cube1");
            Debug.Log("2");
        }
        else if (WebSocketManager.Instance.players.IndexOf(WebSocketManager.Instance.player) == 3)
        {
            dictionary[WebSocketManager.Instance.players[0]] = cube1;
            dictionary[WebSocketManager.Instance.players[1]] = cube2;
            dictionary[WebSocketManager.Instance.players[2]] = cube3;
            dictionary[WebSocketManager.Instance.players[3]] = cube0;
            pulls.Pull(card, "Cube1");
            pulls.Pull(card, "Cube2");
            pulls.Pull(card, "Cube3");
            pulls.Pull(card, "Cube0");
            Debug.Log("3");
        }

        WebSocketManager.Instance.Send("ready:now");
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
                Debug.Log(command[2] + "command2");
                findCard = GameObject.Find(command[2]).GetComponent<card>();
                findCard.LayOthersCard();

            }
            else if (command[0] == "turn")
            {
                Debug.Log("myturn");
                stopButton.SetActive(true);
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
