using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("tagged");
        }
    }

}
