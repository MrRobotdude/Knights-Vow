using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image blackFade;
    
    // Start is called before the first frame update
    void Start()
    {
        blackFade.canvasRenderer.SetAlpha(1.0f);
        fadeIn();
    }

    void fadeIn()
    {
        blackFade.CrossFadeAlpha(0, 0.5f, false);
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}