using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;



public class PostProcessingScript : MonoBehaviour
{
    public bool Reverse = false;
    public PostProcessVolume volume;
    private Vignette _Vin;

    


    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out _Vin);

        _Vin.intensity.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Reverse)
        {
            if (_Vin.intensity.value >= 0)
                _Vin.intensity.value = Mathf.Lerp(_Vin.intensity.value, -3, 0.04f * Time.deltaTime);
        }

        if (Reverse)
        {
            _Vin.intensity.value = Mathf.Lerp(_Vin.intensity.value, 4, 0.04f * Time.deltaTime);
        }


    }
}
