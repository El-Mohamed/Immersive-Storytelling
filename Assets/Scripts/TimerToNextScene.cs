using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerToNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimeLeft = 30;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0)
        {
            SceneManager.LoadScene("brightworld");
        }




    }
}
