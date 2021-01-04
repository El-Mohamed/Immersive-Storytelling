using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject bottom;
    public GameObject Top;
    public float TimeLeft = 5;

    private bool blinkOver = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (blinkOver)
        {
            Top.GetComponent<Animator>().enabled = false;
            bottom.GetComponent<Animator>().enabled = false;
            blinkOver = false;
        }

        else if (!blinkOver && TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
        }

        if (TimeLeft < 0)
        {
            blinkOver = true;
        }

        if (TimeLeft < 1)
        {
            bottom.GetComponent<Renderer>().enabled = false;
            Top.GetComponent<Renderer>().enabled = false;
        }
    }


}
