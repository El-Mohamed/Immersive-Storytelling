using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public GameObject _gameObject;
    public float TimeLeft = 5;
    public bool timer = false;

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {

            _gameObject.GetComponent<PostProcessingScript>().Reverse = true;
            //script.Reverse = true;
            timer = true;
            //SceneManager.LoadScene("brightworld");
            //Debug.Log("tagged");
        }
    }

    void Update()
    {
        if (timer)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft < 0 && TimeLeft > -7.9999999999999999999f)
            {

                _gameObject.GetComponent<Blink>().bottom.GetComponent<Renderer>().enabled = true;
                _gameObject.GetComponent<Blink>().Top.GetComponent<Renderer>().enabled = true;
            }
            else if (TimeLeft < -8)
            {
                SceneManager.LoadScene("brightworld");
                _gameObject.GetComponent<Blink>().bottom.GetComponent<Renderer>().enabled = true;
                _gameObject.GetComponent<Blink>().Top.GetComponent<Renderer>().enabled = true;
            }
        }







    }

}
