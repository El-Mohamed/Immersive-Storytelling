using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound360 : MonoBehaviour
{
    public AudioSource frontAudioSource;
    public AudioSource backAudioSource;
    public AudioSource leftAudioSource;
    public AudioSource rightAudioSource;
    //public AudioClip audioClip;

    private List<AudioSource> allAudioSources;


    private const int FrontAngle = 0;
    private const int RightAngle = 90;
    private const int BackAngle = 180;
    private const int LeftAngle = 270;

    private int lastPlayed = 0;

    // Start is called before the first frame update
    void Start()
    {
        allAudioSources = new List<AudioSource>();
        allAudioSources.Add(frontAudioSource);
        allAudioSources.Add(rightAudioSource);
        allAudioSources.Add(backAudioSource);
        allAudioSources.Add(leftAudioSource);
        StartAllSounds();
    }

    private void StartAllSounds()
    {
        foreach (var audioSource in allAudioSources)
        {
            audioSource.Play();
        }
    }

    private void MuteAllSoundsExpect(int index)
    {
        for (int i = 0; i < allAudioSources.Count; i++)
        {
            if (i != index)
            {
                allAudioSources[i].volume = 0.05f;
            }
            else
            {
                allAudioSources[i].volume = 1f;
                //allAudioSources[i].Play(audioClip);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var currentYAngle = gameObject.transform.rotation.eulerAngles.y;

        if ((currentYAngle < 45 && currentYAngle >= 0) || (currentYAngle > 315 && currentYAngle < 360))
        {
            Debug.Log("Voorkant" + currentYAngle);
            MuteAllSoundsExpect(0);
        }


        if ((currentYAngle < 135 && currentYAngle > 45))
        {
            Debug.Log("Rechts" + currentYAngle);
            MuteAllSoundsExpect(1);

        }

        if ((currentYAngle < 225 && currentYAngle > 135))
        {
            Debug.Log("Achter" + currentYAngle);

            MuteAllSoundsExpect(2);
        }

        if ((currentYAngle < 315 && currentYAngle > 225))
        {
            Debug.Log("Links" + currentYAngle);
            MuteAllSoundsExpect(3);
        }

    }


}
