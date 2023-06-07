using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToSecondStage : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;

    public Text Loading;
    public GameObject gb;
    string[] LoadingText = new string[2];
    public Text sloganText;

    void LoadingRandom()
    {
        LoadingText[0] = "Through Hardwork and Dedication, We Hold Our Future In Our Hands - Bluejack 20-1";
        LoadingText[1] = "Always Try New Things, Overcome All Problems - BlueJack 19-1";
        sloganText.text = LoadingText[Random.Range(0, 1)];
    }

    IEnumerator LoadOperation()
    {

        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("SecondStage");
        while (gameLevel.progress < 1)
        {
            progressBar.fillAmount = gameLevel.progress;
            Loading.text = (gameLevel.progress * 100).ToString() + "%";
            yield return new WaitForEndOfFrame();
        }

    }

    public void LoadLevel()
    {
        LoadingRandom();
        StartCoroutine(LoadOperation());
    }
}
