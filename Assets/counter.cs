using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{

    public static counter Instance;    
    public GameObject pickColor;
    public int count = 0;
    // Start is called before the first frame update 
    void Start()
    {
        pickColor = GameObject.Find("ChangeColor");
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
