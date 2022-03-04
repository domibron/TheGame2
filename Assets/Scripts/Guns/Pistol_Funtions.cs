using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Funtions : MonoBehaviour
{
    //assigns reload an animation
    public Animation reload;

    //when called is plays the reload animation
    public void ReloadAnimation()
    {
        reload.Play();
    }
}
