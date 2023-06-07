using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadCanvas : MonoBehaviour
{
    public GameObject dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterStats.currentHealth <= 0)
        {
            dead.SetActive(true);
        }
    }
}
