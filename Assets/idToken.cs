using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idToken : MonoBehaviour
{

    public static idToken Instance;
    public string id;

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
