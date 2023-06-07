using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;

    Resolution[] resolutions;
    public Image blackFade;
    public Dropdown resolutionDD;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDD.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resolutionDD.AddOptions(options);
        resolutionDD.value = currentResolution;
        resolutionDD.RefreshShownValue();

    }

    public void setQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void setResolution(int resolutionIdx)
    {
        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }

    public void Back()
    {
        setting.SetActive(false);
        menu.SetActive(true);
        blackFade.canvasRenderer.SetAlpha(1.0f);
        fadeIn();
    }
    void fadeIn()
    {
        blackFade.CrossFadeAlpha(0, 2, false);
        Debug.Log("fade");
    }
}
