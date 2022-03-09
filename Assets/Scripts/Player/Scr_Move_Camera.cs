using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Move_Camera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
