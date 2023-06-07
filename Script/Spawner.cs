using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static bool flag = false;
    public GameObject morak;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            Instantiate(morak, transform.position, Quaternion.identity);
            Spawner.flag = false;
        }
    }
}
