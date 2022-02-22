using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera_player : MonoBehaviour
{
    float Sensitivity = 10f;

    public Transform RB;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        RB.Rotate(Vector3.up * MouseX);
    }
}
