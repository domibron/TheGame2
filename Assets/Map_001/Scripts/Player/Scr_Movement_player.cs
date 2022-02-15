using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Movement_player : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        bool w = Input.GetButton("Fire2");

        if (w)
        {
            Vector3 move = new Vector3(0, 0, 1) * speed;
            rb.MovePosition(move);
            Debug.Log("Moved using w key");
        }
    }
}
