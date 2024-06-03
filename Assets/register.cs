using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.EventSystems;


public class register : MonoBehaviour
{
    public Text Mail_field;
    public Text Password_field;
    public Button Go_Back_Button;
    public Button Register_Button;
    public bool logedin = false;

    FirebaseAuth auth;

    // Start is called before the first frame update
    void Start()
    {
        Button goBack = Go_Back_Button.GetComponent<Button>();
        goBack.onClick.AddListener(Go_Back_Click);
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
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }
    void Go_Back_Click()
    {
        SceneManager.UnloadScene("Register");
    }

    async void RegisterClick()
    {
        string email = Mail_field.text.ToString();
        string password = Password_field.text.ToString();

        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            logedin = true;
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
            //string idToken = task.Result;
            Debug.Log(idToken.Instance.id);
            // Send token to your backend via HTTPS
            // ...
        });

        if (logedin)
        {
            EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            eventSystem.enabled = false;
            SceneManager.LoadScene("Start_Page", LoadSceneMode.Additive);
        }

    }
}
