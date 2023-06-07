using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fall : MonoBehaviour
{
    public GameObject dead;
    Vector3 startPosition;
    bool flag = false;
    public Image blackFade;
    public Text text;
    public Text counter;
    public int count = 0;

    [SerializeField]
    AudioClip clip;

    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        audio = GetComponent<AudioSource>();
        counter.text = "Death Counter : 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5f)
        {
            if(!flag)
            {
                flag = true;
                dead.SetActive(true);
                audio.PlayOneShot(clip);
                blackFade.canvasRenderer.SetAlpha(0.0f);
                text.canvasRenderer.SetAlpha(0.0f);
                fadeout();
                StartCoroutine(waitRespawn());
            }

        }
        
    }
    void fadeout()
    {
        blackFade.CrossFadeAlpha(1, 1, false);
        text.CrossFadeAlpha(1, 1, false);
        Count();
    }

    IEnumerator waitRespawn()
    {
        yield return new WaitForSeconds(7.732f);
        PlayerManager.instance.player.GetComponent<CharacterStats>().TakeDamage(PlayerManager.instance.player.GetComponent<CharacterStats>().maxHealth/10);
        transform.position = startPosition;
        dead.SetActive(false);
        flag = false;
    }

    public void Count()
    {
        count++;
        counter.text = "Death Counter : " + count;
    }


}
