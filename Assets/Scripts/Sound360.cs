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

        var angle = gameObject.transform.rotation.eulerAngles.y;

        if ((angle < 45 && angle > 0) || (angle > 315 && angle < 360)) {
            Debug.Log("Voorkant" + angle);
            frontAudioSource.volume = 1f;
            backAudioSource.volume = 0f;
            leftAudioSource.volume = 0f;
            rightAudioSource.volume = 0f;
        }


        if ((angle < 135 && angle > 45))
        {
            Debug.Log("Rechts" + angle);
            frontAudioSource.volume = 0f;
            backAudioSource.volume = 0f;
            leftAudioSource.volume = 0f;
            rightAudioSource.volume = 1f;
        }

        if ((angle < 225 && angle > 135))
        {
            frontAudioSource.volume = 0f;
            backAudioSource.volume = 1f;
            leftAudioSource.volume = 0f;
            rightAudioSource.volume = 0f;
            Debug.Log("Achter" + angle);
        }
      

        if ((angle < 315 && angle > 225))
        {
            frontAudioSource.volume = 0f;
            backAudioSource.volume = 0f;
            leftAudioSource.volume = 1f;
            rightAudioSource.volume = 0f;
            Debug.Log("Links" + angle);
        }




        //Debug.Log(gameObject.transform.rotation.eulerAngles.y);
    }


}
