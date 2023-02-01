using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTETrigger : MonoBehaviour
{
    public GameObject qteObject;
    public static bool qteComplete = false;

    void OnTriggerEnter()
    {
        qteObject.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (qteComplete == true)
        {
            qteObject.SetActive(false);
        }
    }
}
