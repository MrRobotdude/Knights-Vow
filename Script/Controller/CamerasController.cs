using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public float RotSpeed = 4;
    public Transform Target, Player;
    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void Control()
    {
        mouseX += Input.GetAxis("Mouse X") * RotSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotSpeed;
        mouseY = Mathf.Clamp(mouseY, -30, 30);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        if(Input.GetKey(KeyCode.W))
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
