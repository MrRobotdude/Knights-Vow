using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    [SerializeField]
    private AudioClip clips;

    private AudioSource audioSource;
    public GameObject fade;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(waitFade());
    }

    IEnumerator waitFade()
    {
        yield return new WaitForSeconds(1);
        fade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        SaveSystem.SavePlayer(PlayerManager.instance.player.GetComponent<CharacterStats>());
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        audioSource.PlayOneShot(clips);
        PauseMenuScript.GameIsPaused = false;
        Debug.Log("masuk");
    }

    public void No()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuScript.GameIsPaused = false;
    }
}
