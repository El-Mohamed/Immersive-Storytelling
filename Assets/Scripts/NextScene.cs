using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public GameObject _gameObject;
    public float TimeLeft = 5;
    public bool timer = false;
    public Image _Image;

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
                var image = this._Image.GetComponent<Image>();
                var temp = image.color;
                temp.a += 0.0024f;
                image.color = temp;
            }
            else if (TimeLeft < -8)
            {
                SceneManager.LoadScene("brightworld");
            }
        }







    }

}
