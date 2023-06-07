using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadFade : MonoBehaviour
{
    public Image blackFade;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);
        text.canvasRenderer.SetAlpha(0.0f);
        fadeout();
    }

    void fadeout()
    {
        blackFade.CrossFadeAlpha(1, 1, false);
        text.CrossFadeAlpha(1, 1, false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}