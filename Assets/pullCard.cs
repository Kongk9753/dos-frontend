using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class pullCard : MonoBehaviour
    {
        public GameObject parentObj;
        public Transform meeple;
        private Animation anim;

        void OnMouseDown()
        {
            Pull(meeple);
        }

        public void Pull(Transform pullTransform)
        {
            Debug.Log("OnMouseDown");
            parentObj = GameObject.Find("Cube");
            pullTransform = transform.GetChild(0);
            int childCount = parentObj.transform.childCount;
            Debug.Log("Child count: " + childCount);
            pullTransform.parent = parentObj.transform;

            // anim = pullTransform.GetComponent<Animation>();
            // anim.Play("pullCard");  

            pullTransform.position = transform.position + new Vector3(-300f - pullTransform.parent.position.x - pullTransform.position.x + (90f * childCount), 135f, pullTransform.parent.position.z - pullTransform.position.z);
            pullTransform.eulerAngles = new Vector3(0, 90, 40);
            pullTransform.localScale = new Vector3(0.003998217f, 1.011981f, 0.1200001f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

