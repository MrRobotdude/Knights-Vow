using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Showup : MonoBehaviour
{
    public Canvas text;
    public Canvas SaveUI;
    public Image blackFade;
    // Start is called before the first frame update
    void Start()
    {

    }

    public float radius;
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
        if(distance < radius)
        {
            text.gameObject.SetActive(true);
        }
        else text.gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveUI.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenuScript.GameIsPaused = true;
            fadeIn();
        }
    }


    void fadeIn()
    {
        blackFade.CrossFadeAlpha(0, 0.5f, false);
    }
}
