using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MouseLook : MonoBehaviour
{
    public Transform Cam;

    public GameObject player;

    public Rigidbody RidB;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * Time.deltaTime * (1);
        float MouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * (1);

        Cam.transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Quaternion LR = Quaternion.Euler(0, MouseX, 0);

        

        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z);
    }
}

/*
 void Update () {
Translate (Vector3. right * speed * Time. deltaTime);
if (Input. GetKeyDown (KeyCode. W))
localEulerAngles = new Vector3(0,0,90);
}
if (Input. GetKeyDown (KeyCode. D))
localEulerAngles = new Vector3(0,0,0);
}

*/
