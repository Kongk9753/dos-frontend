using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherCard : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject parentObj;
    public Transform cards;

    public void LayOthersCard(string cubeName, string cardName)
    {
        // Load all prefabs from the Resources/test folder
        GameObject[] prefabs = Resources.LoadAll<GameObject>("test");
        // Iterate through the loaded prefabs
        for (int i = 0; i < prefabs.Length; i++)
        {
            Debug.Log(prefabs[i].name + " " + cardName.Split("(")[0] + "cards");
            // Check if the prefab name matches the cardName
            if (prefabs[i].name == cardName.Split("(")[0])
            {
                // Instantiate the prefab
                GameObject prefab = prefabs[i];
                GameObject instance = Instantiate(prefab, cards.position + Vector3.right * i, Quaternion.identity, cards);

                string parentName = cubeName;
                parentObj = GameObject.Find("Deck");
                playerHand = GameObject.Find(cubeName);
                int childCount = playerHand.transform.childCount;
                instance.transform.position = instance.transform.position + new Vector3(0 - instance.transform.position.x, 0 - instance.transform.position.y + (1f * childCount), 1300 - instance.transform.position.z);
                instance.transform.eulerAngles = new Vector3(0, 0, 0);
                instance.transform.parent = parentObj.transform;
                instance.transform.localScale = new Vector3(100f, 0.1f, 150f);
                Destroy(playerHand.transform.GetChild(0).gameObject);

            }
        }
        return;
    }
}
