using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Move_Camera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    void Update()
    {
        //sets the camera position to the camera position on the player
        transform.position = cameraPosition.position;
    }
}
