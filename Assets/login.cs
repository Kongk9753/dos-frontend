using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.EventSystems;

public class login : MonoBehaviour
{
    public Text Email_field;
    public Text Password_field;
    public Button Login_Button;
    public Button Register_Button;
    public GameObject Register_Canvas;
    public GameObject Login_Canvas;
    public bool logedin = false;
    FirebaseAuth auth;

    // Start is called before the first frame update
    void Start()
    {
        Button login = Login_Button.GetComponent<Button>();
        login.onClick.AddListener(LoginClick);
        Button register = Register_Button.GetComponent<Button>();
        register.onClick.AddListener(RegisterClick);


        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                auth = FirebaseAuth.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }
    async void LoginClick()
    {
        string email = Email_field.text.ToString();
        string password = Password_field.text.ToString();
        Debug.Log(email + " " + password);

        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("User signed in successfully.");
                logedin = true;
                Debug.Log(task);
            }
            else
            {
                Debug.LogError($"Failed to sign in: {task.Exception}");
            }
        });

        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        await user.TokenAsync(true).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("TokenAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("TokenAsync encountered an error: " + task.Exception);
                return;
            }
            idToken.Instance.id = task.Result;
            Debug.Log(idToken.Instance.id);

        });


        if (logedin)
        {
            DestroyImmediate(Camera.main.gameObject);
            EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            eventSystem.enabled = false;
            SceneManager.LoadScene("Start_Page", LoadSceneMode.Additive);
        }

    }

    void RegisterClick()
    {
        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        eventSystem.enabled = false;
        SceneManager.LoadScene("Register", LoadSceneMode.Additive);
        DestroyImmediate(Camera.main.gameObject);
    }

}
