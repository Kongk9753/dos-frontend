using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck : MonoBehaviour
{
      public Transform cards;
    public Vector3 rotationAngles; // Define the rotation angles here

    void Start()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("test");
        Debug.Log(prefabs.Length);

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            GameObject instance = Instantiate(prefab, cards.position + Vector3.right * i, Quaternion.identity, cards);

            // Rotate the instantiated prefab
            instance.transform.Rotate(rotationAngles);

            // Adjust the scale of the instantiated prefab if needed
            instance.transform.localScale = new Vector3(1f, 1f, 1f); // Set the scale to desired values
        }
    }
}