using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class scr_camerarotate : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(float X, float Y);

    private float start_mouse_x = 0;
    private float start_mouse_y = 0;
    private bool mousepressed = false;
    private bool firstmousepressed = false;

    private float rotateamount=30;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

       

        start_mouse_x = Input.GetAxis("Mouse X");
        start_mouse_y = Input.GetAxis("Mouse Y");

    }

    // Update is called once per frame
    void Update()
    {

        firstmousepressed = Input.GetMouseButtonDown(0);
        //if (firstmousepressed == true)
        //{
        //    start_mouse_x = Input.GetAxis("Mouse X");
        //    start_mouse_y = Input.GetAxis("Mouse Y");
        //}

        mousepressed = Input.GetMouseButton(0);

       

            float MouseX = (start_mouse_x-Input.GetAxis("Mouse X")) * Time.deltaTime * (rotateamount);
            float MouseY = (start_mouse_y-(Input.GetAxis("Mouse Y")) * Time.deltaTime * (rotateamount));
          
            transform.Rotate(MouseY, MouseX, 0);

           
        


    }

    
}
