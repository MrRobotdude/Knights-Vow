﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;

    public Text Loading;
    string[] LoadingText = new string[2];
    public GameObject gb;
    public Text sloganText;
    private void Start()
    {
        Loading = gb.GetComponent<Text>();
        LoadingRandom();
        StartCoroutine(LoadOperation());
    }
    
    void LoadingRandom()
    {
        LoadingText[0] = "Through Hardwork and Dedication, We Hold Our Future In Our Hands - Bluejack 20-1";
        LoadingText[1] = "Always Try New Things, Overcome All Problems - BlueJack 19-1";
        sloganText.text = LoadingText[Random.Range(0, 1)];
    }

    IEnumerator LoadOperation()
    {
        
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(4);
        while(gameLevel.progress < 1)
        {
            progressBar.fillAmount = gameLevel.progress;
            Loading.text = (gameLevel.progress*100).ToString() + "%";
            yield return new WaitForEndOfFrame();
        }

    }
}
