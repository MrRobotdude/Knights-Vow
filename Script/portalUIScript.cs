using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class portalUIScript : MonoBehaviour
{
    public InputField heightField;
    public InputField widthField;
    public InputField ratioField;
    public GameObject ui;
    public GameObject loadingUI;

    public void validateHeight(string heightStr)
    {
        int height = int.Parse(heightStr);
        height = Mathf.Clamp(height, 10, 15);
        heightField.text = height.ToString();
    }

    public void validateWidth(string widthStr)
    {
        int width = int.Parse(widthStr);
        width = Mathf.Clamp(width, 5, 10);
        widthField.text = width.ToString();
    }

    public void validateRatio(string ratioStr)
    {
        int ratio = int.Parse(ratioStr);
        ratio = Mathf.Clamp(ratio, 2, 5);
        ratioField.text = ratio.ToString();
    }

    public void close()
    {
        Cursor.visible = false;
        ui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuScript.GameIsPaused = false;
    }

    public void moveScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        loadingUI.SetActive(true);

        PauseMenuScript.GameIsPaused = false;
    }
}
