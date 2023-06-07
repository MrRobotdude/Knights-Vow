using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firewing : MonoBehaviour
{
    public GameObject wing;
    public bool flag = false;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (flag)
            {
                wing.SetActive(false);
                flag = false;
                anim.SetFloat("speed", 1);

            }
            else
            {
                wing.SetActive(true);
                flag = true;
                anim.SetFloat("speed", 1.5f);
            }
        }
    }
}