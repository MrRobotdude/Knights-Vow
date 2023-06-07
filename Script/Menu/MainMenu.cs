using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;
    public GameObject loadingScreen;
    public AudioSource bgtheme;

    bool hasSaveFile = false;
    public Button Continue;
    public static bool Load;
    public Image blackFade;

    private void Start()
    {
        CheckSave();
        Continue.interactable = hasSaveFile;
        bgtheme.enabled = true;
    }

    public void StartGame()
    {
        bgtheme.enabled = false;
        Delete();
        menu.SetActive(false);
        loadingScreen.SetActive(true);
    }

    public void LoadGame()
    {
        bgtheme.enabled = false;
        Load = true;
        menu.SetActive(false);
        loadingScreen.SetActive(true);

    }

    public void Settings()
    {
        menu.SetActive(false);
        setting.SetActive(true);
        blackFade.canvasRenderer.SetAlpha(1.0f);
        fadeIn();
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Delete();
        Application.Quit(); 
    }

    void CheckSave()
    {

        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            hasSaveFile = true;
        }
        else hasSaveFile = false;
    }

    public void Delete()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/player.save");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    void fadeIn()
    {
        blackFade.CrossFadeAlpha(0, 2, false);
        Debug.Log("fade");
    }
}

