using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak_Functions : MonoBehaviour
{
    //assigns reload animation
    public Animation reload;

    //when called it plays the animation
    public void ReloadAnimation()
    {
        reload.Play();
    }
}
