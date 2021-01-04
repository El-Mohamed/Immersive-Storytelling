using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAudio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource g;
    GameObject[] Cop;

    List<GameObject> thingsInRange = new List<GameObject>();
    float range = 15;
    void Start()
    {
        g = gameObject.GetComponent<AudioSource>();
        g.volume = 1;
        //g.volume = (float)0.0;


    }

    // Update is called once per frame
    void Update()
    {
        //thingsInRange = [];

        //foreach (GameObject g in)
        //{
        Cop = GameObject.FindGameObjectsWithTag("Cop");
        foreach (var cop in Cop)
        {
            if (Vector3.Distance(cop.transform.position, transform.position) <= range)
            {
                //Debug.Log(Vector3.Distance(cop.transform.position, transform.position));
                g.Stop();
                //g.volume = (float)0.0;

                //thingsInRange.append(g);
            }
        }

        //}

    }


}
