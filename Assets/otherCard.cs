using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherCard : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject parentObj;
    public Transform cards;
    public Vector3 rotationAngles;

    public void LayOthersCard(string cubeName, string cardName)
    {

        Debug.Log("1");

        // Load all prefabs from the Resources/test folder
        GameObject[] prefabs = Resources.LoadAll<GameObject>("test");

        Debug.Log("Number of prefabs loaded: " + prefabs.Length);

        // Iterate through the loaded prefabs
        for (int i = 0; i < prefabs.Length; i++)
        {
            Debug.Log("Checking prefab: " + prefabs[i].name);
            Debug.Log("Checking cardname: " + cardName.Split("(")[0]);

            // Check if the prefab name matches the cardName
            if (prefabs[i].name == cardName.Split("(")[0])
            {
                Debug.Log("Prefab found: " + prefabs[i].name);

                // Instantiate the prefab
                GameObject prefab = prefabs[i];
                Debug.Log("Maybe");
                GameObject instance = Instantiate(prefab, cards.position + Vector3.right * i, Quaternion.identity, cards);
                Debug.Log("Maybe2");


                string parentName = cubeName;
                parentObj = GameObject.Find("Deck");
                playerHand = GameObject.Find(cubeName);
                int childCount = playerHand.transform.childCount;
                Debug.Log("3");
                instance.transform.position = instance.transform.position + new Vector3(0 - instance.transform.position.x, 0 - instance.transform.position.y + (1f * childCount), 1300 - instance.transform.position.z);
                instance.transform.eulerAngles = new Vector3(0, 0, 0);
                instance.transform.parent = parentObj.transform;
                instance.transform.localScale = new Vector3(100f, 0.1f, 150f);
                Destroy(playerHand.transform.GetChild(0).gameObject);

            }
        }
        Debug.Log(cubeName + "parent");


        return;
    }
}
