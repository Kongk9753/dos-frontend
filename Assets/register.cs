using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class register : MonoBehaviour
{
    public Text Mail_field; 
    public Text Username_field; 
    public Text Password_field;    
    public Button Go_Back_Button;
    public Button Register_Button;
   // Start is called before the first frame update
    void Start()
    {
        Button goBack = Go_Back_Button.GetComponent<Button>();
		goBack.onClick.AddListener(Go_Back_Click);
        Button register = Register_Button.GetComponent<Button>();
		register.onClick.AddListener(RegisterClick);
    }
    void Go_Back_Click(){
      SceneManager.UnloadScene("Register");
    }

    void RegisterClick(){
        string mail = Mail_field.text.ToString();
        string username = Username_field.text.ToString();
        string password = Password_field.text.ToString();
        Debug.Log(mail + " " + username + " " + password);
        SceneManager.LoadScene("Start_Page", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Register");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
