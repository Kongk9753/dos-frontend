using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class WebSocketManager : MonoBehaviour
{
    private static WebSocketManager _instance;
    private WebSocket _socket;

    // Define a custom event for message received
    public delegate void MessageReceivedHandler(string message);
    public event MessageReceivedHandler OnMessageReceived;

    // Singleton pattern
    public static WebSocketManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singleton = new GameObject("WebSocketManager");
                _instance = singleton.AddComponent<WebSocketManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that this object persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Connect(string gameCode)
    {
        _socket = new WebSocket("ws://localhost:3000/game/join/" + gameCode);
        _socket.OnOpen += (sender, e) => Debug.Log("WebSocket connected!");
        _socket.OnMessage += (sender, e) =>
        {
            OnMessageReceived?.Invoke(e.Data); // Trigger the custom event when a message is received
        };
        _socket.OnClose += (sender, e) => Debug.Log("WebSocket closed!");
        _socket.OnError += (sender, e) => Debug.LogError("WebSocket error: " + e.Message);

        _socket.Connect();
    }

    public void Send(string message)
    {
        if (_socket != null && _socket.IsAlive)
        {
            _socket.Send(message);
        }
        else
        {
            Debug.LogError("WebSocket connection is not open!");
        }
    }

    public void Disconnect()
    {
        if (_socket != null && _socket.IsAlive)
        {
            _socket.Close();
        }
    }

}