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

        if ((angle < 45 && angle > 0) || (angle > 315 && angle < 360))
            Debug.Log("Voorkant"  + angle );

        if ((angle < 135 && angle > 45))
            Debug.Log("Rechts" + angle);

        if ((angle < 225 && angle > 135))
            Debug.Log("Achter" + angle);

        if ((angle < 315 && angle > 225))
            Debug.Log("Links" + angle);




        //Debug.Log(gameObject.transform.rotation.eulerAngles.y);
    }


}
