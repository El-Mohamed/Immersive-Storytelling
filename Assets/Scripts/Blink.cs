using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject bottom;
    public GameObject Top;
    public float TimeLeft = 4;

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
            Destroy(bottom);
            Destroy(Top);
        }

        TimeLeft -= Time.deltaTime;
        if (TimeLeft < 0)
        {
            blinkOver = true;
        }





    }
}
