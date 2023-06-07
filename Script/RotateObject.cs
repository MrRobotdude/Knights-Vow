using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{

    public Vector3 RotateAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}