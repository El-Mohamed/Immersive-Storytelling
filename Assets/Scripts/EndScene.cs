using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public float counter = 17;
    public Image _Image;
    public Text Text;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        counter -= Time.deltaTime;

        if (counter < 0)
        {
            image();
            text();
        }
    }


    public void text()
    {
        var txt = Text.GetComponent<Text>();
        var temp = txt.color;
        temp.a += 0.0034f;
        txt.color = temp;
    }
    public void image()
    {
        var image = this._Image.GetComponent<Image>();
        var temp = image.color;
        temp.a += 0.0024f;
        image.color = temp;
    }
}
