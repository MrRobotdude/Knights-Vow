using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    Camera target;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target.transform);
        transform.rotation = Quaternion.LookRotation(target.transform.forward);

    }
}
