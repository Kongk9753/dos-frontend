using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public Text Username_field; 
    public Text Password_field;    
    public Button Login_Button;
    public Button Register_Button;

    public GameObject Register_Canvas;
    public GameObject Login_Canvas;

   // Start is called before the first frame update
    void Start()
    {
        Button login = Login_Button.GetComponent<Button>();
		login.onClick.AddListener(LoginClick);
        Button register = Register_Button.GetComponent<Button>();
		register.onClick.AddListener(RegisterClick);
    }
    void LoginClick(){
      string username = Username_field.text.ToString();
      string password = Password_field.text.ToString();
      Debug.Log(username + " " + password);
      DestroyImmediate(Camera.main.gameObject);
      SceneManager.LoadScene("Start_Page", LoadSceneMode.Additive);
    }

    void RegisterClick(){
        SceneManager.LoadScene("Register", LoadSceneMode.Additive);
        DestroyImmediate(Camera.main.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
