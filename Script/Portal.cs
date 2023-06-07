using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject ui;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(PlayerManager.instance.player.transform.position, transform.position);
        if (distance < 5f)
        {
            if (flag == false)
            {
                flag = true;
                ui.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PauseMenuScript.GameIsPaused = true;
            }
        }
        else
        {
            flag = false;
        }
    }

}
