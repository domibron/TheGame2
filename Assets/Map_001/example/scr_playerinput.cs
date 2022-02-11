using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerinput : MonoBehaviour
{
    private float movespeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal_Keyboard");

        float verticalinput = Input.GetAxis("Vertical_Keyboard");

        float finalmovespeed = movespeed * Time.deltaTime;

        transform.Translate(horizontalinput* finalmovespeed,0, verticalinput*finalmovespeed,0);




    }
}
