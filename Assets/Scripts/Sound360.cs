using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound360 : MonoBehaviour
{
    public AudioSource frontAudioSource;
    public AudioSource backAudioSource;
    public AudioSource leftAudioSource;
    public AudioSource rightAudioSource;

    private const int FrontAngle = 0;
    private const int RightAngle = 90;
    private const int BackAngle = 180;
    private const int LeftAngle = 270;


    // Start is called before the first frame update
    void Start()
    {
        frontAudioSource.Play();
        backAudioSource.Play();
        leftAudioSource.Play();
        rightAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(gameObject.transform.rotation.eulerAngles.y);
    }

    
}
